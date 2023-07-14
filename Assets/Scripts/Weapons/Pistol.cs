

public class Pistol : WeaponWithBullets
{
    public override void Init()
    {
        Cooldown = 2f;
        BulletsCount = 3;

        DelayBetweenShoots = 0.1f;

        CreateCharges();
    }
}
