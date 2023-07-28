
public class VIP : PropertyItem
{
    public override void Init()
    {
        Name = "Випка";
        CurrentPrice = 40;

        AddPropertie(PlayerData.Properties.Armor, 5);
        AddPropertie(PlayerData.Properties.Dodge, 5);
        AddPropertie(PlayerData.Properties.Health, 5);
        AddPropertie(PlayerData.Properties.Regeneration, 5);
    }
}
