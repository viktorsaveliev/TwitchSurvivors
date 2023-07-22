public class GlassesEZ : PropertyItem
{
    public override void Init()
    {
        Name = "Очки EZ";
        Price = 250;

        AddPropertie(PlayerData.Properties.Damage, 3);
        AddPropertie(PlayerData.Properties.Armor, 3);
        AddPropertie(PlayerData.Properties.AttackSpeed, 3);
        AddPropertie(PlayerData.Properties.CriticalDamage, 3);
        AddPropertie(PlayerData.Properties.Distance, 3);
    }
}
