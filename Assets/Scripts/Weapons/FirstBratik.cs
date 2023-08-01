
public class FirstBratik : Bratik
{
    public override void Init()
    {
        Name = "Первый братик";
        BasicPrice = CurrentPrice = 40;

        SetCooldown(2);
        SetDamage(25);

        CurrentChargesCount = ChargesCount = 2;
    }

    public override void Improve()
    {
        if (ImprovementLevel >= MAX_IMPROVE_LEVEL) return;
        ImprovementLevel++;

        switch (ImprovementLevel)
        {
            case 1:
                ChargesCount = 3;
                break;

            case 2:
                ChargesCount = 4;
                SetDamage(30);
                break;

            case 3:
                ChargesCount = 5;
                SetCooldown(1f);
                break;

            case 4:
                ChargesCount = 6;
                SetDamage(45);
                break;
        }

        UpdatePrice();
    }
}
