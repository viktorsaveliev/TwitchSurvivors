
public class ShoeBootSneakers : Item
{
    public override void Init()
    {
        ItemName = "�����-�������-���������";
        Price = 100;

        AddPropertie(PlayerData.Properties.MoveSpeed, 5);
    }
}
