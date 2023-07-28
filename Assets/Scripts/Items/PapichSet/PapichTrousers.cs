
public class PapichTrousers : PropertyItem
{
    public override void Init()
    {
        Name = "Штаны Папича";
        CurrentPrice = 100;

        AddPropertie(PlayerData.Properties.Regeneration, 5);
    }
}
