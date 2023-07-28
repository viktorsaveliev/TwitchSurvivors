
public class SwordCharge : PlayerBullet
{
    public override void Init(int damage)
    {
        base.Init(damage);
        LifeTime = 5f;
    }
}
