using UnityEngine;
namespace Test1
{
    public interface IShield
    {
        void Defend();
        static IShield CreateDefault()
        {
            return new Shiled();
        }
    }
    public class Shiled : IShield
    {
        public void Defend()
        {
            Debug.Log("Defend");
        }
    }
    public abstract class ShieldFactory : ScriptableObject
    {
        public abstract IShield CreateShield();
    }
}


