using System;

public class EventPlayer
{
    public static event Action<float, float, float> OnUIUpdateExp;
    public static event Action<float, float> OnUIUpdateHp;
    public static event Action OnUIGameOver;

    public static void UIUpdateExp(float lv, float exp, float defaultExp)
    {
        OnUIUpdateExp?.Invoke(lv, exp, defaultExp);
    }
    public static void UIUpdateHp(float hp, float defaultHp)
    {
        OnUIUpdateHp?.Invoke(hp, defaultHp);
    }
    public static void UIGameOver()
    {
        OnUIGameOver?.Invoke();
    }
}
