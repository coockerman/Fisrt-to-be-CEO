using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public float DefaultSpeed = 5f;
    public float DefaultHp = 5f;
    public DataLVPlayer DataLevelPlayer;

    private float speed = 5f;
    private float hp = 5f;
    private float exp = 0f;
    private LevelData levelData;

    private IPlayerState currentState;
    private Dictionary<IPlayerState, float> activeEffects = new Dictionary<IPlayerState, float>();

    private Rigidbody2D rb;
    private Vector2 movement;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetState(new MovingState());

        hp = DefaultHp;
        exp = 0f;
        if (DataLevelPlayer != null)
        {
            levelData = new LevelData(DataLevelPlayer.levels[1]);
        }
        else
        {
            Debug.LogWarning("No level data found");
        }
    }

    private void Update()
    {
        currentState?.UpdateState(this);
        UpdateActiveEffects();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * (speed * Time.fixedDeltaTime));
    }

    public void SetState(IPlayerState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        if (movement.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movement.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void AddHp(float hpGet)
    {
        hp += hpGet;
        if (hp >= DefaultHp) hp = DefaultHp;
        
        EventPlayer.UIUpdateHp(hp, DefaultHp);
    }

    public void ReduceHp(float hpReduce)
    {
        hp -= hpReduce;
        if (hp <= 0)
        {
            hp = 0;
            GameOver();
        }

        EventPlayer.UIUpdateHp(hp, DefaultHp);
    }

    void AddExp(float expGet)
    {
        exp += expGet;
        if (exp >= levelData.expRequired)
        {
            if (levelData.level < DataLevelPlayer.levels.Length - 1)
                LvUp();
            else
                exp = levelData.expRequired;
        }

        EventPlayer.UIUpdateExp(levelData.level, exp, levelData.expRequired);
    }

    void LvUp()
    {
        exp -= levelData.expRequired;
        levelData = DataLevelPlayer.levels[levelData.level + 1];
        Destroy(transform.GetChild(0).gameObject);
        Instantiate(levelData.bodyPlayer, transform);
    }

    void EatFish(IFish fish)
    {
        if (fish.LvFish <= levelData.level)
        {
            AddExp(fish.ExpCanGet);
            fish.Die();
        }
        else
        {
            fish.Attack(this);
        }
        
    }

    void GameOver()
    {
        EventPlayer.UIGameOver();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(EEntity.Obstacle.ToString()))
        {
            IObstacle obstacle = other.GetComponent<IObstacle>();
            if (obstacle != null)
            {
                AddEffect(obstacle.GetEffectState(), obstacle.TimeEffect);
                obstacle.Die();
            }
        }
        else if (other.CompareTag(EEntity.Fish.ToString()))
        {
            IFish fish = other.transform.parent.GetComponent<IFish>();
            if (fish != null && !(currentState is StunnedState))
            {
                EatFish(fish);
            }
        }
    }


    public void AddEffect(IPlayerState newEffect, float duration)
    {
        if (currentState == null || newEffect.Priority >= currentState.Priority)
        {
            SetState(newEffect);
        }

        if (activeEffects.ContainsKey(newEffect))
        {
            activeEffects[newEffect] = Mathf.Max(activeEffects[newEffect], duration);
        }
        else
        {
            activeEffects.Add(newEffect, duration);
        }
    }

    private void UpdateActiveEffects()
    {
        List<IPlayerState> effectsToRemove = new List<IPlayerState>();

        foreach (IPlayerState effect in new List<IPlayerState>(activeEffects.Keys))
        {
            activeEffects[effect] -= Time.deltaTime;

            if (activeEffects[effect] <= 0)
            {
                effectsToRemove.Add(effect);
            }
        }

        foreach (var effect in effectsToRemove)
        {
            activeEffects.Remove(effect);
        }

        IPlayerState highestPriorityEffect = null;
        foreach (IPlayerState activeEffect in activeEffects.Keys)
        {
            if (highestPriorityEffect == null || activeEffect.Priority > highestPriorityEffect.Priority)
            {
                highestPriorityEffect = activeEffect;
            }
        }

        SetState(highestPriorityEffect ?? new MovingState());
    }
}