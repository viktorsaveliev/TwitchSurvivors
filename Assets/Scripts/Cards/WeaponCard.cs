using UnityEngine;
using UnityEngine.UI;

public class WeaponCard : ShopCard
{
    [SerializeField] private WeaponCardConfig _config;

    [SerializeField] private Image[] _backgrounds;

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

        foreach (Image bg in _backgrounds)
        {
            bg.color = _config.ColorForLevel[_weapon.ImprovementLevel + 1];
        }
    }

}
