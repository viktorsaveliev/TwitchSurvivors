
public class PapichTrousers : Item
{
    public override void Init()
    {
        ItemName = "����� ������";
        Price = 100;

        AddPropertie(PlayerData.Properties.Regeneration, 5);
    }
}
