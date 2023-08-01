using TMPro;
using UnityEngine;

public class WeaponCard : ShopCard
{
    [SerializeField] private WeaponCardConfig _config;
    [SerializeField] private TMP_Text _itemTypeText;

    private Weapon _weapon;

    public override void Init(Item item) 
    {
        base.Init(item);

        _weapon = (Weapon)item;
        UpdateStats();
    }

    public override void UpdateStats()
    {
        base.UpdateStats();

        PropertiesText[0].text = _weapon.GetDescriptionForNextLevel();

        SetColor(_config.ColorForLevel[_weapon.ImprovementLevel + 1]);
        _itemTypeText.color = _config.ColorForLevel[_weapon.ImprovementLevel + 1];
    }
}
