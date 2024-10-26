
public interface IEntity
{
    public string Name { get; }
    public float MoveSpeed { get; }
    public void Movement();
}

public interface IObstacle : IEntity
{
    public EObstacle TypeObstacle { get; }
    public float TimeEffect { get; }
    public void Init(DataObstacle dataEntity);
    public IPlayerState GetEffectState();

}

public interface IFish : IEntity
{
    public EFish TypeFish { get; }
    public int ExpCanGet { get; }
    public int LvFish { get; }
    public void Init(DataFish dataEntity);

}
