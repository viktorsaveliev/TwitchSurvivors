
public class Burmalda : PropertyItem
{
    public override void Init()
    {
        ItemName = "��������";
        Price = 150;

        AddPropertie(PlayerData.Properties.Fortune, 10);
        AddPropertie(PlayerData.Properties.Greed, -10);
    }
}
