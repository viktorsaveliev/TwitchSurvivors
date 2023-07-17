
public class Dodep : Item
{
    public override void Init()
    {
        ItemName = "Додеп";
        Price = 200;

        AddPropertie(PlayerData.Properties.Greed, 10);
        AddPropertie(PlayerData.Properties.MoveSpeed, 10);
        AddPropertie(PlayerData.Properties.Fortune, -10);
    }
}
