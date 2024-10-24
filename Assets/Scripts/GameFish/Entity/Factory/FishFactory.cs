using GameCooking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FishFactory : ASingleton<FishFactory>
{
    private Dictionary<EFish, GameObject> FishPrefabDictionary;
    protected override void Awake()
    {
        base.Awake();

        FishPrefabDictionary = new Dictionary<EFish, GameObject>();
    }
    public void RegisterFish(EFish typeFish, GameObject prefab)
    {
        if (!FishPrefabDictionary.ContainsKey(typeFish))
        {
            FishPrefabDictionary[typeFish] = prefab;
        }
    }

    public IFish CreateFish(DataFish dataFish)
    {
        if (FishPrefabDictionary.TryGetValue(dataFish.TypeFish, out GameObject prefab))
        {
            GameObject fishObject = Instantiate(prefab, gameObject.transform);
            IFish fishComponent = fishObject.GetComponent<IFish>();
            if (fishComponent != null)
            {
                fishComponent.Init(dataFish);
                return fishComponent;
            }
            else
            {
                Debug.LogError("The prefab does not have an IFish component.");
                return null;
            }
        }
        else
        {
            Debug.LogError("Fish Prefab Dictionary not found for: " + dataFish.Name);
            return null;
        }
    }
}
