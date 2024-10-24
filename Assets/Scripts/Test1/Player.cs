using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test1
{
    public class Player : MonoBehaviour
    {

    }
    public class NetworkPlayer
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        NetworkPlayer(string name, int health)
        {
            Name = name;
            Health = health;
        }
        public static NetworkPlayer CreateAndRegister(string name, int health)
        {
            NetworkPlayer player = new NetworkPlayer(name, health);
            //Chay gamelogic
            return player;
        }
    }
}

