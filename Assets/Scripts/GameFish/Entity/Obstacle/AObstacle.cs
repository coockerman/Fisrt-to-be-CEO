using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AObstacle : MonoBehaviour, IObstacle
{
    private EObstacle typeObstacle;
    private string nameObstacle;
    private float timeEffect;
    private float moveSpeed;

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
        Setup();
    }

    protected virtual void Update()
    {
        Movement();
    }
    protected abstract void Setup();
    public abstract void Movement();
}
