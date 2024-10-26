using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float defaultSpeed = 5f;
    private float speed = 5f;

    private IPlayerState _currentState;
    private Dictionary<IPlayerState, float> activeEffects = new Dictionary<IPlayerState, float>();

    private Rigidbody2D rb;
    private Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetState(new MovingState());
    }

    private void Update()
    {
        _currentState?.UpdateState(this);
        UpdateActiveEffects();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * (speed * Time.fixedDeltaTime));
    }

    public void SetState(IPlayerState newState)
    {
        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(EEntity.Obstacle.ToString()))
        {
            IObstacle obstacle = other.GetComponent<IObstacle>();
            if (obstacle != null)
            {
                AddEffect(obstacle.GetEffectState(), obstacle.TimeEffect);
            }
        }
        else if (other.CompareTag(EEntity.Fish.ToString()))
        {
            IFish fish = other.GetComponent<IFish>();
            if (fish != null && !(_currentState is StunnedState))
            {
                EatFish(fish);
            }
        }
    }
    void EatFish(IFish fish)
    {
        Debug.Log("Lv Fish: " + fish.LvFish);
        Debug.Log("Exp Fish: " + fish.ExpCanGet);
    }
    public void AddEffect(IPlayerState newEffect, float duration)
    {
        if (_currentState == null || newEffect.Priority >= _currentState.Priority)
        {
            SetState(newEffect);
        }

        // Cập nhật thời gian nếu hiệu ứng đã tồn tại hoặc thêm hiệu ứng mới vào Dictionary
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

        // Duyệt qua các hiệu ứng và giảm thời gian
        foreach (IPlayerState effect in new List<IPlayerState>(activeEffects.Keys))
        {
            activeEffects[effect] -= Time.deltaTime;

            // Khi thời gian hiệu ứng hết, đưa vào danh sách để xóa
            if (activeEffects[effect] <= 0)
            {
                effectsToRemove.Add(effect);
            }
        }

        // Loại bỏ các hiệu ứng đã hết thời gian ngoài vòng lặp
        foreach (var effect in effectsToRemove)
        {
            activeEffects.Remove(effect);
        }

        // Tìm hiệu ứng có độ ưu tiên cao nhất còn lại
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
