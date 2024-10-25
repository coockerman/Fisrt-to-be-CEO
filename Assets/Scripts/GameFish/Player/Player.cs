using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float defaultSpeed = 5f;
    private float speed = 5f;
    
    private IPlayerState _currentState;
    
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
            // Nhân vật quay sang trái
            transform.localScale = new Vector3(1, 1, 1); 
        }
        else if (movement.x > 0)
        {
            // Nhân vật quay sang phải
            transform.localScale = new Vector3(-1, 1, 1); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(EObstacle.Shoes.ToString()))
        {
            IObstacle obstacle = other.GetComponent<IObstacle>();
            if (obstacle != null)
            {
                if (!(_currentState is StunnedState))
                {
                    SetState(new StunnedState());
                    StartCoroutine(ChangeStateAfterDelay(obstacle.TimeEffect, new MovingState()));
                }
            }
        }else if (other.CompareTag(EFish.NormalFish.ToString()))
        {
            Debug.Log("Impact fish");
            IFish fish = other.GetComponent<IFish>();
            if (fish != null)
            {
                Debug.Log("Impact fish b2");
                if (!(_currentState is StunnedState))
                {
                    EatFish(other.gameObject.GetComponent<IFish>());
                }
            }
        }
    }

    void EatFish(IFish fish)
    {
        Debug.Log("Lv Fish: " + fish.LvFish.ToString());
        Debug.Log("Exp Fish: " + fish.ExpCanGet.ToString());
    }
    private IEnumerator ChangeStateAfterDelay(float timeDelay, IPlayerState newState)
    {
        yield return new WaitForSeconds(timeDelay);

        // Chỉ chuyển trạng thái nếu hiện tại vẫn đang ở StunnedState
        if (_currentState is StunnedState)
        {
            SetState(newState);
        }
    }

    
    private IEnumerator changeState(float timeDelay, IPlayerState newState)
    {
        yield return new WaitForSeconds(timeDelay);
        SetState(newState);
    }
}

