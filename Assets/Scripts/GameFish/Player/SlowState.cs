public class SlowState : IPlayerState
{
    public EObstacle Priority => EObstacle.Waste;
    public void EnterState(Player player)
    {
        player.SetSpeed(player.DefaultSpeed / 2);
    }

    public void UpdateState(Player player)
    {
        player.Move();
    }

    public void ExitState(Player player)
    {
        player.SetSpeed(player.DefaultSpeed);
    }
}