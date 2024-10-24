using UnityEngine;

namespace Test1
{
    [CreateAssetMenu(fileName = "SwordFactory", menuName = "Weapon Factory/Sword")]
    public class SwordFactory : WeaponFactory
    {
        public override IWeapon CreateWeapon()
        {
            return new Sword();
        }
    }
}

