
public class LixxSteeringWheel : Item
{
    public override void Init()
    {
        ItemName = "���� �����";
        Price = 200;

        AddPropertie(PlayerData.Properties.AttackDistance, 10);
        AddPropertie(PlayerData.Properties.MoveSpeed, 10);
        AddPropertie(PlayerData.Properties.AttackSpeed, 5);
    }
}
