
public class Tranquils : PropertyItem
{
    public override void Init()
    {
        ItemName = "���������";
        Price = 200;

        AddPropertie(PlayerData.Properties.Regeneration, 10);
        AddPropertie(PlayerData.Properties.MoveSpeed, 10);
        AddPropertie(PlayerData.Properties.Health, -5);
    }
}
