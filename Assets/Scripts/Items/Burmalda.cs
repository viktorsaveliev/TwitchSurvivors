
public class Burmalda : PropertyItem
{
    public override void Init()
    {
        Name = "Бурмалда";
        CurrentPrice = 10;

        AddPropertie(PlayerData.Properties.Fortune, 10);
        AddPropertie(PlayerData.Properties.Greed, -10);
    }
}
