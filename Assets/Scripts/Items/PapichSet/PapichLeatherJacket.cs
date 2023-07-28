
public class PapichLeatherJacket : PropertyItem
{
    public override void Init()
    {
        Name = "Кожанка Папича";
        CurrentPrice = 100;

        AddPropertie(PlayerData.Properties.Health, 5);
    }
}
