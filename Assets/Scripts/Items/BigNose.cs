public class BigNose : PropertyItem
{
    public override void Init()
    {
        Name = "������� ���";
        CurrentPrice = 30;

        AddPropertie(PlayerData.Properties.Distance, 10);
        AddPropertie(PlayerData.Properties.Fortune, 5);
    }
}
