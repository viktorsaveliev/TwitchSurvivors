public class Agenda : PropertyItem
{
    public override void Init()
    {
        ItemName = "��������";
        Price = 150;

        AddPropertie(PlayerData.Properties.Dodge, 20);
        AddPropertie(PlayerData.Properties.Distance, 10);
    }
}
