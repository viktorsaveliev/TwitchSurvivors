using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private Sprite _icon;

    protected string ItemName;
    protected int Price;
    protected readonly Dictionary<PlayerData.Properties, int> _properties = new();
    
    public Sprite SpriteIcon => _icon;
    public string GetItemName => ItemName;
    public int GetPrice => Price;
    public IReadOnlyDictionary<PlayerData.Properties, int> Properties => _properties;

    private void Awake()
    {
        Init();
    }

    public abstract void Init();

    protected void AddPropertie(PlayerData.Properties propertie, int percent)
    {
        if (!_properties.ContainsKey(propertie))
        {
            _properties.Add(propertie, percent);
        }
    }

    public void Use()
    {
        foreach (var pair in _properties)
        {
            PlayerData.SetPropertieValue(pair.Key, pair.Value);
        }
    }

    public void UnUse()
    {
        foreach (var pair in _properties)
        {
            PlayerData.SetPropertieValue(pair.Key, -pair.Value);
        }
    }
}