public class Midas : PropertyItem
{
    public override void Init()
    {
        Name = "�����";
        Price = 150;

        AddPropertie(PlayerData.Properties.Greed, 15);
        AddPropertie(PlayerData.Properties.AttackSpeed, 5);
        AddPropertie(PlayerData.Properties.Damage, -5);
    }
}
