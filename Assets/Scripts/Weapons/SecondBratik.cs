
public class SecondBratik : Bratik
{
    public override void Init()
    {
        Name = "Второй братик";
        BasicPrice = CurrentPrice = 40;

        SetCooldown(3);
        SetDamage(25);

        CurrentChargesCount = ChargesCount = 3;
    }

    public override void Improve()
    {
        if (ImprovementLevel >= MAX_IMPROVE_LEVEL) return;
        ImprovementLevel++;

        switch (ImprovementLevel)
        {
            case 1:
                ChargesCount = 4;
                break;

            case 2:
                ChargesCount = 5;
                SetDamage(30);
                break;

            case 3:
                ChargesCount = 6;
                SetCooldown(1.5f);
                break;

            case 4:
                SetDamage(45);
                break;
        }

        UpdatePrice();
    }
}
