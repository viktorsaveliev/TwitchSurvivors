
public class Burmalda : PropertyItem
{
    public override void Init()
    {
        Name = "��������";
        CurrentPrice = 20;

        AddPropertie(PlayerData.Properties.Fortune, 10);
        AddPropertie(PlayerData.Properties.Greed, -10);
    }
}
