

public class Pistol : WeaponWithCharges
{
    public override void Init()
    {
        Cooldown = 2f;
        ChargesCount = 3;

        DelayBetweenShoots = 0.1f;

        CreateCharges();
    }
}
