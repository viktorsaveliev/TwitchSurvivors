

public class Subs : PropertyItem
{
    public override void Init()
    {
        Name = "брсър";
        CurrentPrice = 25;

        AddPropertie(PlayerData.Properties.Damage, 5);
        AddPropertie(PlayerData.Properties.AttackSpeed, 5);
        AddPropertie(PlayerData.Properties.CriticalDamage, 5);
        AddPropertie(PlayerData.Properties.Distance, 5);
    }
}
