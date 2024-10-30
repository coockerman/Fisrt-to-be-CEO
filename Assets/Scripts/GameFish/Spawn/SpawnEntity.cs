using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEntity : ASingleton<SpawnEntity>
{
    [SerializeField] Transform leftPos;
    [SerializeField] Transform rightPos;
    [SerializeField] Transform topPos;
    [SerializeField] Transform downPos;

    private readonly List<AFish> fishPool = new List<AFish>();
    private readonly List<AObstacle> obstaclePool = new List<AObstacle>();


    public AFish GetEntityFromPool(DataFish dataFish)
    {
        if (dataFish == null) return null;

        AFish fish = CheckPool(dataFish);
        Vector3 randomPos = GetRandomSpawnPosLeftRight();
    
        if (fish == null)
        {
            IFish newFish = FishFactory.Instance.CreateFish(dataFish);
            fish = newFish as AFish;
            if (fish == null) return null; 
            fish.gameObject.SetActive(false);
        }
        else
        {
            fish.Init(dataFish);
        }
    
        SetFishPositionAndScale(fish, randomPos);
        fish.gameObject.SetActive(true);

        return fish;
    }

    
    public AObstacle GetEntityFromPool(DataObstacle dataObstacle)
    {
        if (dataObstacle == null) return null;

        AObstacle obstacle = CheckPool(dataObstacle);
        Vector3 randomPos = GetRandomSpawnPosTop();
    
        if (obstacle == null)
        {
            IObstacle newObstacle = ObstacleFactory.Instance.CreateObstacle(dataObstacle);
            obstacle = newObstacle as AObstacle;
            if (obstacle == null) return null; 
            obstacle.gameObject.SetActive(false);
        }
        else
        {
            obstacle.Init(dataObstacle);
        }
    
        SetObstaclePositionAndScale(obstacle, randomPos);
        obstacle.gameObject.SetActive(true);

        return obstacle;
    }
    private void SetFishPositionAndScale(AFish fish, Vector3 position)
    {
        fish.transform.position = position;
        fish.transform.localScale = new Vector3(position.x < 0 ? -1 : 1, 1, 1);
    }

    private void SetObstaclePositionAndScale(AObstacle obstacle, Vector3 position)
    {
        obstacle.transform.position = position;
    }
    AFish CheckPool(DataFish dataFish)
    {
        for (int i = 0; i < fishPool.Count; i++)
        {
            AFish fish = fishPool[i];
            if (dataFish.TypeFish == fish.TypeFish)
            {
                fishPool.RemoveAt(i);
                return fish;
            }
        }
        return null;
    }
    AObstacle CheckPool(DataObstacle dataObstacle)
    {
        for (int i = 0; i < obstaclePool.Count; i++)
        {
            AObstacle obstacle = obstaclePool[i];
            if (dataObstacle.TypeObstacle == obstacle.TypeObstacle)
            {
                obstaclePool.RemoveAt(i);
                return obstacle;
            }
        }
        return null;
    }
    Vector3 GetRandomSpawnPosLeftRight()
    {
        float randomX = Random.Range(0, 2);
        var posX = randomX >= 0.5f ? rightPos.position.x : leftPos.position.x;

        float randomY = Random.Range(-12, 12);
        var posY = randomY;
        return new Vector3(posX, posY, 0);
    }

    Vector3 GetRandomSpawnPosTop()
    {
        float posX = Random.Range(rightPos.position.x + 2f, leftPos.position.x - 2f);
        float posY = topPos.position.y;
        return new Vector3(posX, posY, 0);
    }

    
    public void ReturnObjectToPool(AFish fishDie)
    {
        fishDie.gameObject.SetActive(false);
        fishPool.Add(fishDie);
    }

    public void ReturnObjectToPool(AObstacle obstacleDie)
    {
        obstacleDie.gameObject.SetActive(false);
        obstaclePool.Add(obstacleDie);
    }
}