
public class FirstBratik : Bratik
{
    public override void Init()
    {
        Name = "������ ������";
        Price = 500;

        SetCooldown(3);
        SetDamage(25);

        CurrentChargesCount = ChargesCount = 2;
    }

    public override void Improve()
    {
        
    }
}
