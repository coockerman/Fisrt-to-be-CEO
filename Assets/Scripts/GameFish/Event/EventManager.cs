using System;

public class EventManager
{
    public static event Action<float> OnUIUpdateTimeClock;
    public static event Action OnUISetting;
    
    public static void UIUpdateTimeClock(float timeClock)
    {
        OnUIUpdateTimeClock?.Invoke(timeClock);
    }
    public static void UISetting()
    {
        OnUISetting?.Invoke();
    }
}
