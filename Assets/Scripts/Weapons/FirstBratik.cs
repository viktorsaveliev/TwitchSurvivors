
public class FirstBratik : Bratik
{
    public override void Init()
    {
        Name = "Первый братик";
        BasicPrice = CurrentPrice = 40;

        SetCooldown(4);
        SetDamage(25);

        CurrentChargesCount = ChargesCount = 1;
    }

    public override void Improve()
    {
        if (ImprovementLevel >= MAX_IMPROVE_LEVEL) return;
        ImprovementLevel++;

        switch (ImprovementLevel)
        {
            case 1:
                ChargesCount = 2;
                break;

            case 2:
                ChargesCount = 3;
                SetDamage(30);
                break;

            case 3:
                ChargesCount = 4;
                SetCooldown(2f);
                break;

            case 4:
                ChargesCount = 6;
                SetDamage(45);
                break;
        }

        UpdatePrice();
    }
}
