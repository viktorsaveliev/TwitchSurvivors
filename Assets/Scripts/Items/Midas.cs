public class Midas : PropertyItem
{
    public override void Init()
    {
        Name = "Мидас";
        CurrentPrice = 30;

        AddPropertie(PlayerData.Properties.Greed, 15);
        AddPropertie(PlayerData.Properties.AttackSpeed, 5);
        AddPropertie(PlayerData.Properties.Damage, -5);
    }
}
