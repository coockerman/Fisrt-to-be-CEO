public class SlowState : IPlayerState
{
    public void EnterState(Player player)
    {
        player.SetSpeed(player.defaultSpeed / 2);
    }

    public void UpdateState(Player player)
    {
        player.Move();
    }

    public void ExitState(Player player)
    {
        player.SetSpeed(player.defaultSpeed);
    }
}