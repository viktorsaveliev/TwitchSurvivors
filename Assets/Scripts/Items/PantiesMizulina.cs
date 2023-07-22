
public class PantiesMizulina : PropertyItem
{
    public override void Init()
    {
        Name = "Трусы Мизулиной";
        Price = 15;

        AddPropertie(PlayerData.Properties.CriticalDamage, 15);
        AddPropertie(PlayerData.Properties.Armor, -5);
    }
}
