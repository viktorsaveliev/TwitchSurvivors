
public class LixxSteeringWheel : PropertyItem
{
    public override void Init()
    {
        Name = "Руль Ликса";
        CurrentPrice = 20;

        AddPropertie(PlayerData.Properties.Distance, 10);
        AddPropertie(PlayerData.Properties.MoveSpeed, 10);
        AddPropertie(PlayerData.Properties.AttackSpeed, 5);
    }
}
