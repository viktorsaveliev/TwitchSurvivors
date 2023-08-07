
public class AloneViewer : PropertyItem
{
    public override void Init()
    {
        Name = "Единственный зритель";
        CurrentPrice = 40;

        AddPropertie(PlayerData.Properties.Health, 3);
        AddPropertie(PlayerData.Properties.Regeneration, 3);
        AddPropertie(PlayerData.Properties.Damage, 3);
        AddPropertie(PlayerData.Properties.AttackSpeed, 3);
        AddPropertie(PlayerData.Properties.CriticalDamage, 3);
        AddPropertie(PlayerData.Properties.Distance, 3);
        AddPropertie(PlayerData.Properties.Armor, 3);
        AddPropertie(PlayerData.Properties.Dodge, 3);
        AddPropertie(PlayerData.Properties.MoveSpeed, 3);
        AddPropertie(PlayerData.Properties.Fortune, 3);
        AddPropertie(PlayerData.Properties.Greed, 3);
    }
}
