using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntity : ASingleton<SpawnEntity>
{
    [SerializeField] Transform leftPos;
    [SerializeField] Transform rightPos;
    [SerializeField] Transform topPos;
    [SerializeField] Transform downPos;
    
    public IFish SpawnElementEntity(DataFish dataFish)
    {
        if (dataFish == null) return null;

        IFish newFish = FishFactory.Instance.CreateFish(dataFish);
        AFish fishMonoBehaviour = newFish as AFish;
        Vector3 randomPos = GetRandomSpawnPosLeftRight();
        if (fishMonoBehaviour != null)
        {
            fishMonoBehaviour.gameObject.transform.position = randomPos;
            if (randomPos.x < 0) fishMonoBehaviour.transform.localScale = new Vector3(-1, 1, 1);
        }

        return newFish;
    }
    
    public IObstacle SpawnElementEntity(DataObstacle dataObstacle)
    {
        if (dataObstacle == null) return null;

        IObstacle newObstacle = ObstacleFactory.Instance.CreateObstacle(dataObstacle);
        AObstacle ObstacleMonoBehaviour = newObstacle as AObstacle;
        Vector3 randomPos = GetRandomSpawnPosTop();
        if (ObstacleMonoBehaviour != null)
        {
            ObstacleMonoBehaviour.gameObject.transform.position = randomPos;
        }
        return ObstacleMonoBehaviour;
    }
    Vector3 GetRandomSpawnPosLeftRight()
    {
        float randomX = Random.Range(0, 2); 
        var posX = randomX >= 0.5f ? rightPos.position.x : leftPos.position.x;

        float randomY = Random.Range(-13, 13);
        var posY = randomY;
        return new Vector3(posX, posY, 0);
    }

    Vector3 GetRandomSpawnPosTop()
    {
        float posX = Random.Range(rightPos.position.x + 2f, leftPos.position.x - 2f);
        float posY = topPos.position.y;
        return new Vector3(posX, posY, 0);
    }
}
