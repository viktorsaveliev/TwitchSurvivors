
public class PapichTrousers : PropertyItem
{
    public override void Init()
    {
        Name = "����� ������";
        Price = 100;

        AddPropertie(PlayerData.Properties.Regeneration, 5);
    }
}
