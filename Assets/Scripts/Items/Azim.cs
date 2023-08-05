
public class Azim : PropertyItem
{
    public override void Init()
    {
        Name = "Азим";
        CurrentPrice = 15;

        AddPropertie(PlayerData.Properties.Greed, 10);
        AddPropertie(PlayerData.Properties.Health, -1);
        AddPropertie(PlayerData.Properties.Regeneration, -1);
        AddPropertie(PlayerData.Properties.Damage, -1);
        AddPropertie(PlayerData.Properties.AttackSpeed, -1);
    }
}
