using System.Collections.Generic;

public class PlayerData
{
    private static readonly PropertiesData _properties = new();
    private static readonly Money _money = new();

    public enum Properties
    {
        Health,
        Regeneration,
        Damage,
        CriticalDamage,
        AttackSpeed,
        Distance,
        Armor,
        Dodge,
        MoveSpeed,
        Fortune,
        Greed
    }

    private static readonly string[] _propertiesName =
    {
        "Здоровье",
        "Регенерация",
        "Урон",
        "Крит. урон",
        "Перезарядка",
        "Дистанция",
        "Броня",
        "Уклонение",
        "Скорость",
        "Удача",
        "Жадность"
    };

    public static string[] PropertiesName => _propertiesName;

    public static List<PropertyItem> Items => _properties.Items;

    public void Init()
    {
        _properties.Init();
        _money.Init();
    }

    public static int GetPropertieValue(Properties properties) => _properties.Properties[properties];

    public static void SetPropertieValue(Properties properties, int percents)
    {
        if (percents < -50 || percents > 50) return;
        _properties.Properties[properties] += percents;
    }

    public static float CalculatePropertieValue(Properties propertie, float value, bool increasePercentToValue = true) 
        => _properties.CalculatePropertieValue(propertie, value, increasePercentToValue);
}
