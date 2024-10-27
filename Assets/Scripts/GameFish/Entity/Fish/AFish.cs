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
    private float endPointX;
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
    private void OnEnable()
    {
        endPointX = -gameObject.transform.position.x;
    }
    protected virtual void Update()
    {
        Movement();
        CheckEndPoint();
    }

    protected abstract void Setup();
    public abstract void Movement();

    public abstract void Die();


    public virtual void Attack(Player player)
    {
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void CheckEndPoint()
    {
        if (endPointX > 0)
        {
            if (transform.position.x > endPointX) Die();
        }
        else if(endPointX < 0)
        {
            if (transform.position.x < endPointX) Die();
        }
    }
}