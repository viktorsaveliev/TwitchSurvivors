public class Agenda : PropertyItem
{
    public override void Init()
    {
        Name = "��������";
        CurrentPrice = 50;

        AddPropertie(PlayerData.Properties.Dodge, 20);
        AddPropertie(PlayerData.Properties.Distance, 10);
    }
}
