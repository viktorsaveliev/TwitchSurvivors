
public class DumplingsPlate : Item
{
    public override void Init()
    {
        ItemName = "������� ���������";
        Price = 200;

        AddPropertie(PlayerData.Properties.Regeneration, 15);
        AddPropertie(PlayerData.Properties.MoveSpeed, -5);
    }
}
