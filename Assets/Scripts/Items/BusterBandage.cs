public class BusterBandage : Item
{
    public override void Init()
    {
        ItemName = "Повязка Бустера";
        Price = 150;

        AddPropertie(PlayerData.Properties.Damage, 5);
        AddPropertie(PlayerData.Properties.AttackSpeed, 5);
        AddPropertie(PlayerData.Properties.CriticalDamage, 5);

        AddPropertie(PlayerData.Properties.Armor, -5);
        AddPropertie(PlayerData.Properties.Health, -5);
        AddPropertie(PlayerData.Properties.Regeneration, -3);
    }
}
