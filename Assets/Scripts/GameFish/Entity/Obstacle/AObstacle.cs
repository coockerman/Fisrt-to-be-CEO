using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AObstacle : MonoBehaviour, IObstacle
{
    private EObstacle typeObstacle;
    private string nameObstacle;
    private float timeEffect;
    private float moveSpeed;
    private float endPointY = 0;

    public EObstacle TypeObstacle => typeObstacle;
    public string Name => nameObstacle;
    public float TimeEffect => timeEffect;
    public float MoveSpeed => moveSpeed;

    public void Init(DataObstacle dataEntity)
    {
        typeObstacle = dataEntity.TypeObstacle;
        nameObstacle = dataEntity.Name;
        timeEffect = dataEntity.TimeEffect;
        moveSpeed = dataEntity.MoveSpeed;
        gameObject.tag = EEntity.Obstacle.ToString();
    }

    private void OnEnable()
    {
        Setup();
        endPointY = - gameObject.transform.position.y;
    }

    protected virtual void Update()
    {
        Movement();
        CheckEndPoint();
    }

    protected virtual void Setup()
    {
        gameObject.AddComponent<BoxCollider2D>();
    }
    public abstract void Movement();
    
    public abstract IPlayerState GetEffectState();
    public abstract void Die();
    
    // ReSharper disable Unity.PerformanceAnalysis
    void CheckEndPoint()
    {
        if (endPointY > 0)
        {
            if (transform.position.y > endPointY) Die();
        }
        else if(endPointY < 0)
        {
            if (transform.position.y < endPointY) Die();
        }
    }
}
