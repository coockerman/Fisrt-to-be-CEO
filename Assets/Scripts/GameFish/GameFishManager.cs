using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFishManager : MonoBehaviour
{
    [SerializeField] List<DataFish> dataListFish;
    [SerializeField] List<DataObstacle> dataListObstacle;

    [SerializeField] GameObject normalFishPrefab;
    [SerializeField] GameObject expFishPrefab;

    [SerializeField] GameObject shoesObstaclePrefab;
    [SerializeField] GameObject wasteObstaclePrefab;

    private void Start()
    {
        FishFactory.Instance.RegisterFish(EFish.NormalFish, normalFishPrefab);
        FishFactory.Instance.RegisterFish(EFish.ExpFish, expFishPrefab);

        ObstacleFactory.Instance.RegisterObstacle(EObstacle.Waste, wasteObstaclePrefab);
        ObstacleFactory.Instance.RegisterObstacle(EObstacle.Shoes, shoesObstaclePrefab);

        StartCoroutine(ToSpawnEntity(dataListFish, 3f));
        StartCoroutine(ToSpawnEntity(dataListObstacle, 2f));
    }
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator ToSpawnEntity(List<DataFish> dataEntity, float timeDelay)
    {
        while(true)
        {
            RanSpawnEntity(dataEntity);
            yield return new WaitForSeconds(timeDelay);
        }
    }
    IEnumerator ToSpawnEntity(List<DataObstacle> dataEntity, float timeDelay)
    {
        while(true)
        {
            RanSpawnEntity(dataEntity);
            yield return new WaitForSeconds(timeDelay);
        }
    }
    void RanSpawnEntity(List<DataFish> dataEntity)
    {
        int ranListEntity = Random.Range(0, dataEntity.Count);
        SpawnEntity.Instance.GetEntityFromPool(dataEntity[ranListEntity]);
    }
    // ReSharper disable Unity.PerformanceAnalysis
    void RanSpawnEntity(List<DataObstacle> dataEntity)
    {
        int ranListEntity = Random.Range(0, dataEntity.Count);
        SpawnEntity.Instance.GetEntityFromPool(dataEntity[ranListEntity]);
    }
}
