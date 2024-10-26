using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoesObstacle : AObstacle
{
    protected override void Setup()
    {
        gameObject.AddComponent<BoxCollider2D>();
    }
    public override void Movement()
    {
        Vector3 moveDirection = new Vector3(0, -MoveSpeed * Time.deltaTime, 0);
        transform.position += moveDirection;
    }

    public override IPlayerState GetEffectState()
    {
        return new StunnedState();
    }
}
