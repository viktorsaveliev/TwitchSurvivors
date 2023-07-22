using System.Collections.Generic;

public abstract class PropertyItem : Item
{
    protected readonly Dictionary<PlayerData.Properties, int> _properties = new();
    public IReadOnlyDictionary<PlayerData.Properties, int> Properties => _properties;

    public override void Use()
    {
        foreach (var pair in _properties)
        {
            PlayerData.AppendPropertieValue(pair.Key, pair.Value);
        }

        PlayerData.Items.Add(this);
    }

    public override void UnUse()
    {
        foreach (var pair in _properties)
        {
            PlayerData.AppendPropertieValue(pair.Key, -pair.Value);
        }
    }

    protected void AddPropertie(PlayerData.Properties propertie, int percent)
    {
        if (!_properties.ContainsKey(propertie))
        {
            _properties.Add(propertie, percent);
        }
    }
}