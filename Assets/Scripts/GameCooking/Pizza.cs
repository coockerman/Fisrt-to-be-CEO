using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCooking
{
    public interface IDish
    {
        void Init(float calories, int price);
        void GetDetails();
    }

    public class Pizza : MonoBehaviour, IDish
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
            Debug.Log($"Pizza: {calories} calories, Price: {price}$");
        }
    }
}

