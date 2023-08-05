using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character Data Config")]
public class CharacterDataConfig : ScriptableObject
{
    [Header("Base")]
    [SerializeField] private string _name;
    [SerializeField] private Sprite _iconLeft;
    [SerializeField] private Sprite _iconRight;
    [SerializeField] private string _description;

    [Header("Properties")]
    [SerializeField, Range(-30, 30)] private int _health;
    [SerializeField, Range(0, 30)] private int _regeneration;
    [SerializeField, Range(-30, 30)] private int _damage;
    [SerializeField, Range(0, 30)] private int _criticalDamage;
    [SerializeField, Range(-30, 30)] private int _attackSpeed;
    [SerializeField, Range(-30, 30)] private int _distance;
    [SerializeField, Range(0, 30)] private int _armor;
    [SerializeField, Range(0, 30)] private int _dodge;
    [SerializeField, Range(-30, 30)] private int _moveSpeed;
    [SerializeField, Range(0, 30)] private int _fortune;
    [SerializeField, Range(0, 30)] private int _greed;

    public string Name => _name;
    public Sprite IconLeft => _iconLeft;
    public Sprite IconRight => _iconRight;
    public string Description => _description;

    public int Health => _health;
    public int Regeneration => _regeneration;
    public int Damage => _damage;
    public int CriticalDamage => _criticalDamage;
    public int AttackSpeed => _attackSpeed;
    public int Distance => _distance;
    public int Armor => _armor;
    public int Dodge => _dodge;
    public int MoveSpeed => _moveSpeed;
    public int Fortune => _fortune;
    public int Greed => _greed;

    public Dictionary<string, int> GetProperties()
    {
        Dictionary<string, int> nonZeroProperties = new();

        if (_health != 0)
        {
            nonZeroProperties.Add("Здоровье", _health);
        }

        if (_regeneration != 0)
        {
            nonZeroProperties.Add("Регенерация", _regeneration);
        }

        if (_damage != 0)
        {
            nonZeroProperties.Add("Урон", _damage);
        }

        if (_criticalDamage != 0)
        {
            nonZeroProperties.Add("Критический урон", _criticalDamage);
        }

        if (_attackSpeed != 0)
        {
            nonZeroProperties.Add("Скорость атаки", _attackSpeed);
        }

        if (_distance != 0)
        {
            nonZeroProperties.Add("Дистанция", _distance);
        }

        if (_armor != 0)
        {
            nonZeroProperties.Add("Броня", _armor);
        }
        
        if (_dodge != 0)
        {
            nonZeroProperties.Add("Уклонение", _dodge);
        }

        if (_moveSpeed != 0)
        {
            nonZeroProperties.Add("Скорость", _moveSpeed);
        }
        
        if (_fortune != 0)
        {
            nonZeroProperties.Add("Удача", _fortune);
        }
        
        if (_greed != 0)
        {
            nonZeroProperties.Add("Жадность", _greed);
        }

        return nonZeroProperties;
    }

}
