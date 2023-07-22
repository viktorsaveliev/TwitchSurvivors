
public class PapichLeatherJacket : PropertyItem
{
    public override void Init()
    {
        Name = "Кожанка Папича";
        Price = 100;

        AddPropertie(PlayerData.Properties.Health, 5);
    }
}
