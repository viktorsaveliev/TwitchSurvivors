using System.Collections.Generic;

public class PropertiesData
{
    public readonly Dictionary<PlayerData.Properties, int> Properties = new();
    public readonly List<Item> Items = new();

    public void Init()
    {
        Properties.Add(PlayerData.Properties.Health, 0);
        Properties.Add(PlayerData.Properties.Regeneration, 0);
        Properties.Add(PlayerData.Properties.Damage, 0);
        Properties.Add(PlayerData.Properties.CriticalDamage, 25);
        Properties.Add(PlayerData.Properties.AttackSpeed, 0);
        Properties.Add(PlayerData.Properties.AttackDistance, 0);
        Properties.Add(PlayerData.Properties.Armor, 0);
        Properties.Add(PlayerData.Properties.Dodge, 0);
        Properties.Add(PlayerData.Properties.MoveSpeed, 0);
        Properties.Add(PlayerData.Properties.Fortune, 0);
        Properties.Add(PlayerData.Properties.Greed, 0);
    }

    public float CalculatePropertieValue(PlayerData.Properties propertie, float value, bool increasePercentToValue = true)
    {
        if (PlayerData.GetPropertieValue(propertie) == 0) return value;

        float percentIncrease = PlayerData.GetPropertieValue(propertie) / 100f;
        float newValue;

        if (increasePercentToValue)
        {
            newValue = value + (value * percentIncrease);
        }
        else
        {
            newValue = value - (value * percentIncrease);
        }

        return newValue;
    }
}
