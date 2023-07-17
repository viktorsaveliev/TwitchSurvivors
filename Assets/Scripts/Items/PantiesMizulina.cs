
public class PantiesMizulina : Item
{
    public override void Init()
    {
        ItemName = "Трусы Мизулиной";
        Price = 15;

        AddPropertie(PlayerData.Properties.CriticalDamage, 15);
        AddPropertie(PlayerData.Properties.Armor, -5);
    }
}
