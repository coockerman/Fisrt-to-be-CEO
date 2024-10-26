public interface IPlayerState
{
    EObstacle Priority { get; }
    void EnterState(Player player);
    void UpdateState(Player player);
    void ExitState(Player player);
}