
public class PapichLeatherJacket : PropertyItem
{
    public override void Init()
    {
        ItemName = "������� ������";
        Price = 100;

        AddPropertie(PlayerData.Properties.Health, 5);
    }
}
