
public class PapichLeatherJacket : PropertyItem
{
    public override void Init()
    {
        ItemName = "Кожанка Папича";
        Price = 100;

        AddPropertie(PlayerData.Properties.Health, 5);
    }
}
