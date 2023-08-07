
public class Dodep : PropertyItem
{
    public override void Init()
    {
        Name = "Додеп";
        CurrentPrice = 35;

        AddPropertie(PlayerData.Properties.Greed, 10);
        AddPropertie(PlayerData.Properties.MoveSpeed, 10);
        AddPropertie(PlayerData.Properties.Fortune, -10);
    }
}
