public class BigNose : PropertyItem
{
    public override void Init()
    {
        ItemName = "������� ���";
        Price = 100;

        AddPropertie(PlayerData.Properties.Distance, 10);
        AddPropertie(PlayerData.Properties.Fortune, 5);
    }
}
