
public class PapichLeatherJacket : PropertyItem
{
    public override void Init()
    {
        Name = "������� ������";
        CurrentPrice = 20;

        AddPropertie(PlayerData.Properties.Health, 5);
    }
}
