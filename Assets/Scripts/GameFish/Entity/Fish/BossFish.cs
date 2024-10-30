using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFish : AFish
{
    [SerializeField] List<DataFish> listFishData;
    [SerializeField] private AudioClip groulSFX;
    private bool isOver = false;
    private bool isEndPoint = false;
    protected override void Update()
    {
        if (!isOver)
        {
            Movement();
            CheckEndPoint();
        }
        else
        {
            MovementOver();
            CheckEndPointOver();
        }
    }
    protected override void SetEndPoint()
    {
        if(transform.position.x < 0)
            endPointX = transform.position.x + 7f;
        else if(transform.position.x > 0)
            endPointX = transform.position.x - 7f;
    }

    void SetEndPointOver()
    {
        if(transform.position.x < 0)
            endPointX = transform.position.x - 15f;
        else if(transform.position.x > 0)
            endPointX = transform.position.x + 15f;
    }
    public override void Movement()
    {
        if (!isEndPoint)
        {
            float head = 1;
            if (transform.localScale.x < 0) head = 1;
            else head = -1;
            Vector3 moveDirection = new Vector3(MoveSpeed * Time.deltaTime * head, 0, 0);
            transform.position += moveDirection / 2;
        }
    }
    void MovementOver()
    {
        if (!isEndPoint)
        {
            float head = 1;
            if (transform.localScale.x < 0) head = 1;
            else head = -1;
            Vector3 moveDirection = new Vector3(MoveSpeed * Time.deltaTime * -head, 0, 0);
            transform.position += moveDirection;
        }
    }
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator SpawnFishAttack(int MaxSpawnCount)
    {
        isEndPoint = true;
        int spawnCount = 0;
        int maxSpawn = MaxSpawnCount;
        int rand = Random.Range(0, listFishData.Count);
        SoundManager.Instance.PlayAudioSource(groulSFX);
        
        yield return new WaitForSeconds(0.5f);
        while (spawnCount < maxSpawn)
        {
            yield return new WaitForSeconds(0.6f);
            spawnCount++;
            SpawnEntity.Instance.GetEntityFromPool(listFishData[rand]);
        }
        yield return new WaitForSeconds(2f);
        
        SetEndPointOver();
        isOver = true;
        isEndPoint = false;
    }
    public override void Attack(Player player)
    {
        player.ReduceHp(3);
    }
    protected override void CheckEndPoint()
    {
        if (endPointX < 0)
        {
            if (transform.position.x > endPointX && !isEndPoint) SpawnBoss();
        }
        else if(endPointX > 0)
        {
            if (transform.position.x < endPointX && !isEndPoint) SpawnBoss();
        }
    }

    void SpawnBoss()
    {
        int ran = Random.Range(4, 7);
        StartCoroutine(SpawnFishAttack(ran));
    }
    // ReSharper disable Unity.PerformanceAnalysis
    protected void CheckEndPointOver()
    {
        if (endPointX > 0)
        {
            if (transform.position.x > endPointX) Die();
        }
        else if(endPointX < 0)
        {
            if (transform.position.x < endPointX) Die();
        }
    }
    public override void Die()
    {
        isOver = false;
        isEndPoint = false;
        SpawnEntity.Instance.ReturnObjectToPool(this);
    }
}
