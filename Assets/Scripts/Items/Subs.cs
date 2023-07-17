

public class Subs : Item
{
    public override void Init()
    {
        ItemName = "брсър";
        Price = 500;

        AddPropertie(PlayerData.Properties.Damage, 5);
        AddPropertie(PlayerData.Properties.AttackSpeed, 5);
        AddPropertie(PlayerData.Properties.CriticalDamage, 5);
        AddPropertie(PlayerData.Properties.AttackDistance, 5);
    }
}
