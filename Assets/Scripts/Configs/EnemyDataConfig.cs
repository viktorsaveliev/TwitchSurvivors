using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy Data Config")]
public class EnemyDataConfig : ScriptableObject
{
    [SerializeField, Range(0.5f, 20)] private float _speed;
    [SerializeField, Range(1, 70)] private int _damage;
    [SerializeField, Range(3, 15000)] private int _health;
    [SerializeField, Range(0, 5)] private int _shield;
    [SerializeField, Range(0, 3)] private int _enemyLevel;

    public float Speed => _speed;
    public int Damage => _damage;
    public int Health => _health;
    public int Shield => _shield;
    public int EnemyLevel => _enemyLevel;
}
