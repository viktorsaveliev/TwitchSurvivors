public class Midas : Item
{
    public override void Init()
    {
        ItemName = "Мидас";
        Price = 150;

        AddPropertie(PlayerData.Properties.Greed, 15);
        AddPropertie(PlayerData.Properties.AttackSpeed, 5);
        AddPropertie(PlayerData.Properties.Damage, -5);
    }
}
