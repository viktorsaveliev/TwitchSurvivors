
public class PantiesMizulina : Item
{
    public override void Init()
    {
        ItemName = "����� ���������";
        Price = 15;

        AddPropertie(PlayerData.Properties.CriticalDamage, 15);
        AddPropertie(PlayerData.Properties.Armor, -5);
    }
}
