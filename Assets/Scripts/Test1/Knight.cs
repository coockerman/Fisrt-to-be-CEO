using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test1
{
    public class Knight : MonoBehaviour
    {
        [SerializeField] WeaponFactory weaponFactory;
        IWeapon weapon = IWeapon.CreateDefault();
        private void Start()
        {
            if (weaponFactory != null)
            {
                weapon = weaponFactory.CreateWeapon();
            }
            Attack();
        }
        public void Attack() => weapon?.Attack();
    }
}

