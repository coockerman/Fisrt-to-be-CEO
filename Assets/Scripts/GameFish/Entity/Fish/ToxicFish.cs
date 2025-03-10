﻿using UnityEngine;

public class ToxicFish : AFish
{
    protected override void InitAnimation(DataFish dataFish)
    {
        
    }

    protected override void Setup()
    {
        
    }
    public override void Movement()
    {
        float head = 1;
        if (transform.localScale.x < 0) head = 1;
        else head = -1;
        Vector3 moveDirection = new Vector3(MoveSpeed * Time.deltaTime * head, 0, 0);
        transform.position += moveDirection;
    }
    public override void Attack(Player player)
    {
        player.ReduceHp(2f);
        Die();
    }
    public override void Die()
    {
        SpawnEntity.Instance.ReturnObjectToPool(this);
    }
}
