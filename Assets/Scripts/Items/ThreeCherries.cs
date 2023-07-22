public class ThreeCherries : PropertyItem
{
    public override void Init()
    {
        Name = "Три вишенки";
        Price = 50;

        AddPropertie(PlayerData.Properties.Greed, 10);
        AddPropertie(PlayerData.Properties.Fortune, -10);
    }
}
