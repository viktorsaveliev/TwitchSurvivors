public class ProtectivePotion : PropertyItem
{
    public override void Init()
    {
        Name = "������";
        CurrentPrice = 30;

        AddPropertie(PlayerData.Properties.Armor, 10);
    }
}
