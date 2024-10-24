using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntity : ASingleton<SpawnEntity>
{
    [SerializeField] Transform leftPos;
    [SerializeField] Transform rightPos;
    public IFish SpawnElementEntity(DataFish DataFish)
    {
        if (DataFish == null) return null;

        IFish newFish = FishFactory.Instance.CreateFish(DataFish);
        MonoBehaviour fishMonoBehaviour = newFish as MonoBehaviour;
        Vector3 randomPos = GetRandomSpawnPos();
        fishMonoBehaviour.gameObject.transform.position = randomPos;
        if (randomPos.x < 0) fishMonoBehaviour.transform.localScale = new Vector3(-1, 1, 1);
        return newFish;
    }
    Vector3 GetRandomSpawnPos()
    {
        float posX, posY;
        
        float randomX = Random.Range(0, 2); 
        if(randomX >= 0.5f) posX = rightPos.position.x;
        else posX = leftPos.position.x;

        float randomY = Random.Range(-13, 13);
        posY = randomY;
        return new Vector3(posX, posY, 0);
    }
    public void SpawnElementEntity(List<DataObstacle> listObstacle)
    {

    }
}
