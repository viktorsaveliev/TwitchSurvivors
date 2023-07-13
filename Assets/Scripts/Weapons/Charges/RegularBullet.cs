public class RegularBullet : Charge
{
    public override void Init()
    {
        base.Init();

        Speed = 15f;
        Damage = 5;

        LifeTime = 1.5f;
    }
}
