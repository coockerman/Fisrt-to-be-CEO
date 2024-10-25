using Spine.Unity;
using UnityEngine;

[CreateAssetMenu(fileName = "DataFish", menuName = "Entity/dataFish")]
public class DataFish : DataEntity
{
    public EFish TypeFish = EFish.NormalFish;
    public int ExpCanGet;
    public int LvFish = 0;
    public SkeletonDataAsset SkeletonData;
}

