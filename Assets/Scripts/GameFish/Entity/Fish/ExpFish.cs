using UnityEngine;

public class ExpFish : AFish
{
    public override void Movement()
    {
        float head = 1;
        if (transform.localScale.x < 0) head = 1;
        else head = -1;
        Vector3 moveDirection = new Vector3(MoveSpeed * Time.deltaTime * head, 0, 0);
        transform.position += (moveDirection * 2);
    }

    public override void Die()
    {
        SpawnEntity.Instance.ReturnObjectToPool(this);
    }
}
