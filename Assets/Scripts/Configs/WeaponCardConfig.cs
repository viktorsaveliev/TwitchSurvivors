using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Card Config", menuName = "Cards/Weapon Card Config")]
public class WeaponCardConfig : ScriptableObject
{
    [SerializeField] private Color[] _colorsForLevel;

    public Color[] ColorForLevel => _colorsForLevel;
}
