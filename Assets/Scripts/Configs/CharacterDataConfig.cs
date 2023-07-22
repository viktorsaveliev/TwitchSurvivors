using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character Data Config")]
public class CharacterDataConfig : ScriptableObject
{
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

}
