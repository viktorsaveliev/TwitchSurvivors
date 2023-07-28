
public class DumplingsPlate : PropertyItem
{
    public override void Init()
    {
        Name = "Тарелка пельменей";
        CurrentPrice = 15;

        AddPropertie(PlayerData.Properties.Regeneration, 15);
        AddPropertie(PlayerData.Properties.MoveSpeed, -5);
    }
}
