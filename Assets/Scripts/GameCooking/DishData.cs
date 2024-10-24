using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCooking
{

    public class DishData : ScriptableObject
    {
        public string dishName;       // Tên món ăn
        public float calories;        // Calo của món ăn
        public int price;             // Giá món ăn
        public Sprite dishImage;      // Hình ảnh món ăn
    }
}
