
public class RedStone : Item
{
    public override void Init()
    {
        ItemName = "Рыжий камень";
        Price = 400;

        AddPropertie(PlayerData.Properties.Damage, 20);
        AddPropertie(PlayerData.Properties.AttackDistance, 30);

        AddPropertie(PlayerData.Properties.MoveSpeed, -20);
        AddPropertie(PlayerData.Properties.Health, -10);
    }
}
