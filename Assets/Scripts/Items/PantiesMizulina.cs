
public class PantiesMizulina : PropertyItem
{
    public override void Init()
    {
        Name = "����� ���������";
        CurrentPrice = 3;

        AddPropertie(PlayerData.Properties.CriticalDamage, 15);
        AddPropertie(PlayerData.Properties.Armor, -5);
    }
}
