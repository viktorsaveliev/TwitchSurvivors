
public class OnlyViewer : Item
{
    public override void Init()
    {
        ItemName = "Единственный зритель";
        Price = 500;

        AddPropertie(PlayerData.Properties.Health, 1);
        AddPropertie(PlayerData.Properties.Regeneration, 1);
        AddPropertie(PlayerData.Properties.Damage, 1);
        AddPropertie(PlayerData.Properties.AttackSpeed, 1);
        AddPropertie(PlayerData.Properties.CriticalDamage, 1);
        AddPropertie(PlayerData.Properties.AttackDistance, 1);
        AddPropertie(PlayerData.Properties.Armor, 1);
        AddPropertie(PlayerData.Properties.Dodge, 1);
        AddPropertie(PlayerData.Properties.MoveSpeed, 1);
        AddPropertie(PlayerData.Properties.Fortune, 1);
        AddPropertie(PlayerData.Properties.Greed, 1);
    }
}
