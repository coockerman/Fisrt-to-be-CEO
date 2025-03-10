using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public float DefaultSpeed = 5f;
    public float DefaultHp = 5f;
    public DataLVPlayer DataLevelPlayer;

    public AudioClip ImpactSFX;
    public AudioClip DamageSFX;
    public AudioClip EatingSFX;
    public AudioClip GameOverSFX;
    public AudioClip WinnerSFX;
    public AudioClip LvUpSFX;

    public ParticleSystem ParticleLvUp;
    
    private float speed = 5f;
    private float hp = 5f;
    private float exp = 0f;
    private LevelData levelData;

    private IPlayerState currentState;
    private Dictionary<IPlayerState, float> activeEffects = new Dictionary<IPlayerState, float>();

    private Rigidbody2D rb;
    private Vector2 movement;

    private bool isCanReduce = true;
    

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
        Vector2 newPosition = rb.position + movement * (speed * Time.fixedDeltaTime);

        // Giới hạn vị trí trong khoảng đã xác định
        newPosition.x = Mathf.Clamp(newPosition.x, -25f, 25f); // Giới hạn trục X
        newPosition.y = Mathf.Clamp(newPosition.y, -15f, 15f); // Giới hạn trục Y

        // Di chuyển đến vị trí mới
        rb.MovePosition(newPosition);
    }

    void SetCanReduce()
    {
        isCanReduce = true;
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
        if (hp > 0 && isCanReduce)
        {
            hp -= hpReduce;
            isCanReduce = false;
            Invoke(nameof(SetCanReduce), 0.5f);
        }
        if(hp <= 0)
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
        AddHp(1);
        exp -= levelData.expRequired;
        levelData = DataLevelPlayer.levels[levelData.level + 1];
        
        if (levelData.level == 3) EventManager.SpawnExpFish();
        if (levelData.level == 4) EventManager.SpawnBoss();
        
        ParticleLvUp.Play();
        SoundManager.Instance.PlayAudioSource(LvUpSFX);
        Destroy(transform.GetChild(2).gameObject);
        Instantiate(levelData.bodyPlayer, transform);
    }


    void EatFish(IFish fish)
    {
        if (fish.LvFish <= levelData.level)
        {
            if (fish.TypeFish == EFish.BossFish)
            {
                fish.Die();
                GameWinner();
                return;
            }

            SoundManager.Instance.PlayAudioSource(EatingSFX);
            AddExp(fish.ExpCanGet);
            fish.Die();
        }
        else
        {
            SoundManager.Instance.PlayAudioSource(DamageSFX);
            fish.Attack(this);
        }
    }

    void GameOver()
    {
        SoundManager.Instance.PlayGameOverSound(GameOverSFX);
        EventPlayer.UIGameOver();
        gameObject.SetActive(false);
    }

    void GameWinner()
    {
        SoundManager.Instance.PlayGameOverSound(WinnerSFX);
        EventPlayer.UIWinner();
        gameObject.SetActive(false);
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
            if (fish != null)
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

        SoundManager.Instance.PlayAudioSource(ImpactSFX);
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