public class BigNose : Item
{
    public override void Init()
    {
        ItemName = "Большой нос";
        Price = 100;

        AddPropertie(PlayerData.Properties.AttackDistance, 10);
        AddPropertie(PlayerData.Properties.Fortune, 5);
    }
}
