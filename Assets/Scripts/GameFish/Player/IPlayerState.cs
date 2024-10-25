public interface IPlayerState
{
    void EnterState(Player player);
    void UpdateState(Player player);
    void ExitState(Player player);
}