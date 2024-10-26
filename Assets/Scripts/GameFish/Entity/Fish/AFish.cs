using Spine.Unity;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AFish : MonoBehaviour, IFish
{
    private EFish typeFish;
    private string nameFish;
    private int expCanGet;
    private int lvFish;
    private float moveSpeed;
    private SkeletonDataAsset skeletonData;

    public EFish TypeFish => typeFish; 
    public string Name => nameFish; 
    public int ExpCanGet => expCanGet;
    public int LvFish => lvFish;
    public float MoveSpeed => moveSpeed;

    public void Init(DataFish dataFish)
    {
        typeFish = dataFish.TypeFish;
        nameFish = dataFish.Name;
        expCanGet = dataFish.ExpCanGet;
        lvFish = dataFish.LvFish;
        moveSpeed = dataFish.MoveSpeed;
        skeletonData = dataFish.SkeletonData;
        gameObject.GetComponent<SkeletonAnimation>().skeletonDataAsset = dataFish.SkeletonData;
        gameObject.GetComponent<SkeletonAnimation>().Initialize(true);
        gameObject.tag = EEntity.Fish.ToString();
        Setup();
    }

    protected virtual void Update()
    {
        Movement();
    }
    protected abstract void Setup();
    public abstract void Movement();
    
}
