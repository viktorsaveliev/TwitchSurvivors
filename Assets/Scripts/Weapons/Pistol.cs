

public class Pistol : Weapon
{
    public override void Init()
    {
        Cooldown = 2f;
        Distance = 10f;

        ChargesCount = 3;

        DelayBetweenShoots = 0.2f;

        CreateCharges();
    }
}
