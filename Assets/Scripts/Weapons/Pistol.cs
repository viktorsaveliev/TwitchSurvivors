

public class Pistol : ShootableWeapon
{
    public override void Init()
    {
        SetCooldown(2f);
        SetDamage(5);

        ChargesCount = 3;
        DelayBetweenShoots = 0.5f;

        CreateCharges(Damage);
    }
}
