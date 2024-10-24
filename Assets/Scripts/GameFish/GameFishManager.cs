using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFishManager : MonoBehaviour
{
    [SerializeField] List<DataFish> DataListFish;
    [SerializeField] List<DataObstacle> DataListObstacle;

    [SerializeField] GameObject NormalFishPrefab;
    [SerializeField] GameObject ExpFishPrefab;

    [SerializeField] GameObject ShoesObstaclePrefab;
    [SerializeField] GameObject WasteObstaclePrefab;

    private void Start()
    {
        FishFactory.Instance.RegisterFish(EFish.NormalFish, NormalFishPrefab);
        FishFactory.Instance.RegisterFish(EFish.ExpFish, ExpFishPrefab);

        ObstacleFactory.Instance.RegisterObstacle(EObstacle.Waste, WasteObstaclePrefab);
        ObstacleFactory.Instance.RegisterObstacle(EObstacle.Shoes, ShoesObstaclePrefab);

        StartCoroutine(SpawnFish(DataListFish));
    }
    IEnumerator SpawnFish(List<DataFish> dataEntity)
    {
        while(true)
        {
            RanSpawnFish(dataEntity);
            yield return new WaitForSeconds(3f);
        }
    }
    void RanSpawnFish(List<DataFish> dataEntity)
    {
        int ranListFish = Random.Range(0, DataListFish.Count);
        SpawnEntity.Instance.SpawnElementEntity(dataEntity[ranListFish]);

    }
}
