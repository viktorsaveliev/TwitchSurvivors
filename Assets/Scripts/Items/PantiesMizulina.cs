
public class PantiesMizulina : PropertyItem
{
    public override void Init()
    {
        Name = "����� ���������";
        Price = 15;

        AddPropertie(PlayerData.Properties.CriticalDamage, 15);
        AddPropertie(PlayerData.Properties.Armor, -5);
    }
}
