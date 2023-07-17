
public class PapichGlasses : Item
{
    public override void Init()
    {
        ItemName = "Очки Папича";
        Price = 100;

        AddPropertie(PlayerData.Properties.AttackDistance, 5);
    }
}
