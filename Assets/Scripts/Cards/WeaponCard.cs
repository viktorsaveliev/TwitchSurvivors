using UnityEngine;
using UnityEngine.UI;

public class WeaponCard : ShopCard
{
    [SerializeField] private Image[] _stars;
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

        PropertiesText[0].text = $"����: <color=grey>{_weapon.Damage} ��.</color>";
        PropertiesText[1].text = $"�����������: <color=grey>{_weapon.Cooldown} ���.</color>";
        PropertiesText[2].text = string.Empty; // $"�������: <color=grey>{_weapon.ChargesCount} ��.</color>";
        //PropertiesText[3].text = string.Empty;

        for (int i = 0; i < _stars.Length; i++)
        {
            _stars[i].color = _weapon.ImprovementLevel+1 >= i ? Color.green : Color.grey;
        }
    }

}
