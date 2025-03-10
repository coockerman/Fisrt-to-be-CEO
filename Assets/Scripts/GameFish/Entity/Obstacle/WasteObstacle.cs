using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteObstacle : AObstacle
{
    public override void Movement()
    {
        Vector3 moveDirection = new Vector3(0, -MoveSpeed * Time.deltaTime, 0);
        transform.position += moveDirection;
    }

    public override void Die()
    {
        SpawnEntity.Instance.ReturnObjectToPool(this);
    }

    public override IPlayerState GetEffectState()
    {
        return new SlowState();
    }
}
