
public class SecondBratik : Bratik
{
    public override void Init()
    {
        Name = "Второй братик";
        Price = 500;

        SetCooldown(4);
        SetDamage(15);

        CurrentChargesCount = ChargesCount = 3;
    }

    public override void Improve()
    {

    }
}
