public class MovingState : IPlayerState
{
    public EObstacle Priority => EObstacle.None;

    public void EnterState(Player player)
    {
        player.SetSpeed(player.DefaultSpeed);
    }

    public void UpdateState(Player player)
    {
        player.Move();
    }

    public void ExitState(Player player)
    {
        
    }
}