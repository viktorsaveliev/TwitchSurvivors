
public class RedStone : PropertyItem
{
    public override void Init()
    {
        Name = "����� ������";
        CurrentPrice = 35;

        AddPropertie(PlayerData.Properties.Damage, 20);
        AddPropertie(PlayerData.Properties.Distance, 30);

        AddPropertie(PlayerData.Properties.MoveSpeed, -20);
        AddPropertie(PlayerData.Properties.Health, -10);
    }
}
