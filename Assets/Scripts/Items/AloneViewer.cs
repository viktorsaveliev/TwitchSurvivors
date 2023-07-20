
public class AloneViewer : PropertyItem
{
    public override void Init()
    {
        ItemName = "Единственный зритель";
        Price = 200;

        AddPropertie(PlayerData.Properties.Health, 1);
        AddPropertie(PlayerData.Properties.Regeneration, 1);
        AddPropertie(PlayerData.Properties.Damage, 1);
        AddPropertie(PlayerData.Properties.AttackSpeed, 1);
        AddPropertie(PlayerData.Properties.CriticalDamage, 1);
        AddPropertie(PlayerData.Properties.Distance, 1);
        AddPropertie(PlayerData.Properties.Armor, 1);
        AddPropertie(PlayerData.Properties.Dodge, 1);
        AddPropertie(PlayerData.Properties.MoveSpeed, 1);
        AddPropertie(PlayerData.Properties.Fortune, 1);
        AddPropertie(PlayerData.Properties.Greed, 1);
    }
}
