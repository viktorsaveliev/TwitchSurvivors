
public class SecondBratik : Bratik
{
    public override void Init()
    {
        Name = "������ ������";
        Price = 500;

        SetCooldown(4);
        SetDamage(15);

        CurrentChargesCount = ChargesCount = 3;
    }

    public override void Improve()
    {

    }
}
