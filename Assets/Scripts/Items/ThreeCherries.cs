public class ThreeCherries : Item
{
    public override void Init()
    {
        ItemName = "��� �������";
        Price = 50;

        AddPropertie(PlayerData.Properties.Greed, 10);
        AddPropertie(PlayerData.Properties.Fortune, -10);
    }
}
