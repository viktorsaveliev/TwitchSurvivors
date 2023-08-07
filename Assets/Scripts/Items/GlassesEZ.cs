public class GlassesEZ : PropertyItem
{
    public override void Init()
    {
        Name = "Очки EZ";
        CurrentPrice = 60;

        AddPropertie(PlayerData.Properties.Damage, 5);
        AddPropertie(PlayerData.Properties.Armor, 5);
        AddPropertie(PlayerData.Properties.AttackSpeed, 5);
        AddPropertie(PlayerData.Properties.CriticalDamage, 5);
        AddPropertie(PlayerData.Properties.Distance, 5);
    }
}
