using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCooking
{
    public class DishFactory
    {
        private Dictionary<string, GameObject> dishPrefabs;

        public DishFactory()
        {
            // Khởi tạo dictionary chứa prefab các món ăn
            dishPrefabs = new Dictionary<string, GameObject>();
        }

        // Thêm prefab vào dictionary
        public void RegisterDish(string dishName, GameObject prefab)
        {
            if (!dishPrefabs.ContainsKey(dishName))
            {
                dishPrefabs[dishName] = prefab;
            }
        }

        // Tạo món ăn từ DishData
        public IDish CreateDish(DishData dishData)
        {
            if (dishPrefabs.TryGetValue(dishData.dishName, out GameObject prefab))
            {
                GameObject dishObject = Object.Instantiate(prefab); // Tạo món ăn từ prefab
                IDish dishComponent = dishObject.GetComponent<IDish>();
                dishComponent.Init(dishData.calories, dishData.price);
                return dishComponent;
            }
            else
            {
                Debug.LogError("Dish prefab not found for: " + dishData.dishName);
                return null;
            }
        }
    }
}

