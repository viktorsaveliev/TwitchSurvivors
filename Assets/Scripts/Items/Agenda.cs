public class Agenda : Item
{
    public override void Init()
    {
        ItemName = "��������";
        Price = 150;

        AddPropertie(PlayerData.Properties.Dodge, 20);
        AddPropertie(PlayerData.Properties.AttackDistance, 10);
    }
}
