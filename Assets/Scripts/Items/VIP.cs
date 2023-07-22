
public class VIP : PropertyItem
{
    public override void Init()
    {
        Name = "Випка";
        Price = 250;

        AddPropertie(PlayerData.Properties.Armor, 5);
        AddPropertie(PlayerData.Properties.Dodge, 5);
        AddPropertie(PlayerData.Properties.Health, 5);
        AddPropertie(PlayerData.Properties.Regeneration, 5);
    }
}
