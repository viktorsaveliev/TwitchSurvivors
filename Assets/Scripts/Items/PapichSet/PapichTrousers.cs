
public class PapichTrousers : PropertyItem
{
    public override void Init()
    {
        Name = "Штаны Папича";
        CurrentPrice = 20;

        AddPropertie(PlayerData.Properties.Regeneration, 5);
    }
}
