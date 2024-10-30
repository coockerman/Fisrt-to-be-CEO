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
    protected float endPointX;
    private Sprite sprite;
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
        sprite = dataFish.Sprite;
        InitAnimation(dataFish);
        gameObject.tag = EEntity.Fish.ToString();
    }
    protected virtual void InitAnimation(DataFish dataFish)
    {
        gameObject.GetComponent<SkeletonAnimation>().skeletonDataAsset = dataFish.SkeletonData;
        gameObject.GetComponent<SkeletonAnimation>().Initialize(true);
        var skeletonAnimation = gameObject.GetComponent<SkeletonAnimation>();

        string[] animationNames = { "animation", "boi" };

        foreach (var animationName in animationNames)
        {
            if (skeletonAnimation.Skeleton.Data.FindAnimation(animationName) != null)
            {
                skeletonAnimation.AnimationName = animationName;
                break;
            }
        }
    }
    private void OnEnable()
    {
        Setup();
        SetEndPoint();
    }
    protected virtual void Update()
    {
        Movement();
        CheckEndPoint();
    }
    protected virtual void SetEndPoint()
    {
        endPointX = -gameObject.transform.position.x;
    }
    protected virtual void Setup()
    {
        Transform childTransform = gameObject.transform.GetChild(0);
    
        SpriteRenderer spriteRenderer = childTransform.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite;
        }
    
        PolygonCollider2D polygonCollider = childTransform.GetComponent<PolygonCollider2D>();
        
        if (polygonCollider == null)
        {
            childTransform.gameObject.AddComponent<PolygonCollider2D>();
        }
        else
        {
            Destroy(polygonCollider);
            childTransform.gameObject.AddComponent<PolygonCollider2D>();
        }
    }
    public abstract void Movement();

    public abstract void Die();

    public virtual void Attack(Player player)
    {
    }

    // ReSharper disable Unity.PerformanceAnalysis
    protected virtual void CheckEndPoint()
    {
        if ((endPointX > 0 && transform.position.x > endPointX) || 
            (endPointX < 0 && transform.position.x < endPointX))
        {
            Die();
        }
    }
}