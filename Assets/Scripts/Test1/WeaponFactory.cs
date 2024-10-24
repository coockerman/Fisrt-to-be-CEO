using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test1
{
    public interface IWeapon
    {
        void Attack();
        static IWeapon CreateDefault()
        {
            return new Sword();
        }
    }

    public class Sword : IWeapon
    {
        public void Attack()
        {
            Debug.Log("Sword Attak");
        }
    }
    public class Bow : IWeapon
    {
        public void Attack()
        {
            Debug.Log("Bow Attak");
        }
    }

    public abstract class WeaponFactory : ScriptableObject
    {
        public abstract IWeapon CreateWeapon();
    }
}

