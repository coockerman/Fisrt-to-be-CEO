using UnityEngine;

public class ExpFish : AFish
{
    private void Update()
    {
        Movement();
    }
    public override void Movement()
    {
        float head = 1;
        if (transform.localScale.x < 0) head = 1;
        else head = -1;
        Vector3 moveDirection = new Vector3(MoveSpeed * Time.deltaTime * head, 0, 0);
        transform.position += moveDirection;
    }
}
