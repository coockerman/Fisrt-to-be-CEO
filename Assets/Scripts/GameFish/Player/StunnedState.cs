public class StunnedState : IPlayerState
{
    public void EnterState(Player player)
    {
        player.SetSpeed(0);
    }

    public void UpdateState(Player player)
    {
        
    }

    public void ExitState(Player player)
    {
        player.SetSpeed(player.defaultSpeed);
    }
}