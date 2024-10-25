public class BossFish : AFish
{
    protected override void Setup()
    {
        gameObject.tag = EFish.BossFish.ToString();
    }
    public override void Movement()
    {

    }

}
