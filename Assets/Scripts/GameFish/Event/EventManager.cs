using System;

public class EventManager
{
    public static event Action OnUITime;
    public static event Action OnUISetting;
    
    public static void UITime()
    {
        OnUITime?.Invoke();
    }

    public static void UISetting()
    {
        OnUISetting?.Invoke();
    }
}
