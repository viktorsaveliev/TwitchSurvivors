
public class Tranquils : PropertyItem
{
    public override void Init()
    {
        Name = "���������";
        CurrentPrice = 45;

        AddPropertie(PlayerData.Properties.Regeneration, 10);
        AddPropertie(PlayerData.Properties.MoveSpeed, 10);
        AddPropertie(PlayerData.Properties.Health, -5);
    }
}
