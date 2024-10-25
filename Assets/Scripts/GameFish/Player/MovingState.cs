public class MovingState : IPlayerState
{
    public void EnterState(Player player)
    {
        player.SetSpeed(player.defaultSpeed);
    }

    public void UpdateState(Player player)
    {
        player.Move();
    }

    public void ExitState(Player player)
    {
        
    }
}