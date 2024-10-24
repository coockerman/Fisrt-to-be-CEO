using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCooking
{
    public class Salad : MonoBehaviour, IDish
    {
        public float calories;
        public int price;

        public void Init(float calories, int price)
        {
            this.calories = calories;
            this.price = price;
        }

        public void GetDetails()
        {
            Debug.Log($"Salad: {calories} calories, Price: {price}$");
        }
    }
}
