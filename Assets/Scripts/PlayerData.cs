using System.Collections.Generic;

public class PlayerData
{
    public static CharacterDataConfig SelectedCharacter;
    public static PlayerSettings Settings = new();

    private static readonly PropertiesData _properties = new();
    private readonly Money _money = new();

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

    public static void AppendPropertieValue(Properties properties, int percents)
    {
        if (percents < -50 || percents > 50 || percents == 0) return;
        _properties.Properties[properties] += percents;
    }

    public static void ResetProperties()
    {
        for (int i = 0; i < _properties.Properties.Count; i++)
        {
            _properties.Properties[(Properties)i] = 0;
        }
    }

    public static float CalculateValueWithPropertie(Properties propertie, float value, bool increasePercentToValue = true) 
        => _properties.CalculateValueWithPropertie(propertie, value, increasePercentToValue);
}
