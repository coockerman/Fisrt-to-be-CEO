using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCooking
{
    public class GameManager : MonoBehaviour
    {
        public DishData[] dishDataArray; // Mảng các món ăn từ Scriptable Object
        public GameObject pizzaPrefab;    // Prefab cho Pizza
        public GameObject saladPrefab;    // Prefab cho Salad

        private DishFactory dishFactory;

        void Start()
        {
            dishFactory = new DishFactory();

            // Đăng ký các prefab vào factory
            dishFactory.RegisterDish("Pizza", pizzaPrefab);
            dishFactory.RegisterDish("Salad", saladPrefab);

            // Tạo món ăn từ Scriptable Object
            foreach (var dishData in dishDataArray)
            {
                IDish dish = dishFactory.CreateDish(dishData);
                dish?.GetDetails(); // Gọi phương thức để in chi tiết món ăn
            }
        }
    }
}
