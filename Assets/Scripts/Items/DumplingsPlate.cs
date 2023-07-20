
public class DumplingsPlate : PropertyItem
{
    public override void Init()
    {
        ItemName = "Тарелка пельменей";
        Price = 200;

        AddPropertie(PlayerData.Properties.Regeneration, 15);
        AddPropertie(PlayerData.Properties.MoveSpeed, -5);
    }
}
