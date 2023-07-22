
public class StrogoYT : PropertyItem
{
    public override void Init()
    {
        Name = "Ютуб Строго";
        Price = 100;

        AddPropertie(PlayerData.Properties.Greed, 20);
        AddPropertie(PlayerData.Properties.Health, -1);
        AddPropertie(PlayerData.Properties.Regeneration, -1);
        AddPropertie(PlayerData.Properties.Damage, -1);
        AddPropertie(PlayerData.Properties.AttackSpeed, -1);
        AddPropertie(PlayerData.Properties.CriticalDamage, -1);
        AddPropertie(PlayerData.Properties.Distance, -1);
        AddPropertie(PlayerData.Properties.Armor, -1);
        AddPropertie(PlayerData.Properties.Dodge, -1);
        AddPropertie(PlayerData.Properties.MoveSpeed, -1);
        AddPropertie(PlayerData.Properties.Fortune, -1);
    }
}
