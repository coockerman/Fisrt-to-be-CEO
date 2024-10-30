using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class TimeSpawn
{
    public float timeOpenFish;
    public int lvFish;

    public TimeSpawn()
    {
    }

    public TimeSpawn(float timeOpenFish, int lvFish)
    {
        this.timeOpenFish = timeOpenFish;
        this.lvFish = lvFish;
    }
}

public class GameFishManager : MonoBehaviour
{
    [SerializeField] List<DataFish> dataListFish;
    [SerializeField] List<DataFish> dataListFishBoss;
    [SerializeField] List<DataFish> dataListFishExp;
    [SerializeField] List<DataObstacle> dataListObstacle;

    [SerializeField] List<TimeSpawn> listTimeSpawn;

    [SerializeField] GameObject normalFishPrefab;
    [SerializeField] GameObject expFishPrefab;
    [SerializeField] GameObject bossFishPrefab;
    [SerializeField] GameObject toxicFishPrefab;

    [SerializeField] GameObject shoesObstaclePrefab;
    [SerializeField] GameObject wasteObstaclePrefab;

    [SerializeField] float timeSpawnNormalFish = 2f;
    [SerializeField] float timeSpawnBossFish = 25f;
    [SerializeField] float timeSpawnRandExpFishMin = 8f;
    [SerializeField] float timeSpawnRandExpFishMax = 10f;
    [SerializeField] float timeSpawnObstacle = 3.5f;

    private int partSpawn = 0;
    private float timeInGame = 0;
    private float timeCounter = 0f;

    private void Start()
    {
        FishFactory.Instance.RegisterFish(EFish.NormalFish, normalFishPrefab);
        FishFactory.Instance.RegisterFish(EFish.ExpFish, expFishPrefab);
        FishFactory.Instance.RegisterFish(EFish.BossFish, bossFishPrefab);
        FishFactory.Instance.RegisterFish(EFish.ToxicFish, toxicFishPrefab);

        ObstacleFactory.Instance.RegisterObstacle(EObstacle.Waste, wasteObstaclePrefab);
        ObstacleFactory.Instance.RegisterObstacle(EObstacle.Shoes, shoesObstaclePrefab);

        StartCoroutine(ToSpawnEntity(dataListFish, timeSpawnNormalFish));
        StartCoroutine(ToSpawnEntity(dataListObstacle, timeSpawnObstacle));
    }

    private void OnEnable()
    {
        EventManager.OnSpawnBoss += SpawnBoss;
        EventManager.OnSpawnExpFish += SpawnExpFish;
    }

    private void OnDisable()
    {
        EventManager.OnSpawnBoss -= SpawnBoss;
        EventManager.OnSpawnExpFish -= SpawnExpFish;
    }

    private void Update()
    {
        UpdateTimeClock();
    }

    //Update UI clock after 1 second
    void UpdateTimeClock()
    {
        timeCounter += Time.deltaTime;

        if (timeCounter >= 1f)
        {
            timeInGame += 1f;
            timeCounter = 0f;

            EventManager.UIUpdateTimeClock(timeInGame);
            if (listTimeSpawn.Count - 1 > partSpawn)
            {
                if (timeInGame >= listTimeSpawn[partSpawn].timeOpenFish)
                {
                    partSpawn++;
                }
            }
        }
    }

    void SpawnBoss()
    {
        StartCoroutine(ToSpawnEntity(dataListFishBoss, timeSpawnBossFish));
    }

    void SpawnExpFish()
    {
        StartCoroutine(ToSpawnEntity(dataListFishExp, timeSpawnRandExpFishMin, timeSpawnRandExpFishMax));
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator ToSpawnEntity(List<DataFish> dataEntity, float timeDelay)
    {
        while (true)
        {
            RanSpawnEntity(dataEntity);
            yield return new WaitForSeconds(timeDelay);
        }
    }
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator ToSpawnEntity(List<DataFish> dataEntity, float timeDelayRanMin, float timeDelayRanMax)
    {
        while (true)
        {
            float rand = Random.Range(timeDelayRanMin, timeDelayRanMax);
            RanSpawnEntity(dataEntity);
            yield return new WaitForSeconds(rand);
        }
    }
    IEnumerator ToSpawnEntity(List<DataObstacle> dataEntity, float timeDelay)
    {
        while (true)
        {
            RanSpawnEntity(dataEntity);
            yield return new WaitForSeconds(timeDelay);
        }
    }


    void RanSpawnEntity(List<DataFish> dataEntity)
    {
        if (dataEntity.Count == 1)
        {
            SpawnEntity.Instance.GetEntityFromPool(dataEntity[0]);
        }
        else
        {
            if (listTimeSpawn[partSpawn].lvFish == 1)
            {
                SpawnEntity.Instance.GetEntityFromPool(dataEntity[0]);
            }
            else
            {
                int ranListEntity = Random.Range(0, listTimeSpawn[partSpawn].lvFish);
                SpawnEntity.Instance.GetEntityFromPool(dataEntity[ranListEntity]);
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void RanSpawnEntity(List<DataObstacle> dataEntity)
    {
        if (dataEntity.Count == 1)
        {
            SpawnEntity.Instance.GetEntityFromPool(dataEntity[0]);
        }
        else
        {
            int ranListEntity = Random.Range(0, dataEntity.Count);
            SpawnEntity.Instance.GetEntityFromPool(dataEntity[ranListEntity]);
        }
    }
}