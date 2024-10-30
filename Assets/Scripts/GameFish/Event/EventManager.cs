using System;

public class EventManager
{
    public static event Action<float> OnUIUpdateTimeClock;
    public static event Action OnSpawnExpFish;
    public static event Action OnSpawnBoss;
    
    public static void UIUpdateTimeClock(float timeClock)
    {
        OnUIUpdateTimeClock?.Invoke(timeClock);
    }

    public static void SpawnExpFish()
    {
        OnSpawnExpFish?.Invoke();
    }
    public static void SpawnBoss()
    {
        OnSpawnBoss?.Invoke();
    }
}
