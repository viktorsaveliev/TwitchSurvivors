
public class FirstBratik : Bratik
{
    public override void Init()
    {
        Name = "Первый братик";
        Price = 500;

        SetCooldown(3);
        SetDamage(25);

        CurrentChargesCount = ChargesCount = 2;
    }

    public override void Improve()
    {
        
    }
}
