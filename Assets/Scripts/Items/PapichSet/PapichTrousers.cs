
public class PapichTrousers : PropertyItem
{
    public override void Init()
    {
        Name = "����� ������";
        CurrentPrice = 100;

        AddPropertie(PlayerData.Properties.Regeneration, 5);
    }
}
