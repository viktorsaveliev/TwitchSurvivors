
public class PapichTrousers : PropertyItem
{
    public override void Init()
    {
        ItemName = "Штаны Папича";
        Price = 100;

        AddPropertie(PlayerData.Properties.Regeneration, 5);
    }
}
