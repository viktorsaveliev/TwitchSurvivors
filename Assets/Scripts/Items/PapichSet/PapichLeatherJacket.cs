
public class PapichLeatherJacket : PropertyItem
{
    public override void Init()
    {
        Name = "������� ������";
        Price = 100;

        AddPropertie(PlayerData.Properties.Health, 5);
    }
}
