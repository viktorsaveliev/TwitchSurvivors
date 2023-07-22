using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private RegularEnemy _enemyPrefab;
    [SerializeField] private Transform _containerForEnemies;

    private readonly List<Enemy> _enemies = new();
    public IReadOnlyList<Enemy> Enemies => _enemies;

    public event Action<Enemy> OnEnemyCreated;

    public RegularEnemy CreateRegularEnemy(Vector3 position)
    {
        RegularEnemy enemy = Instantiate(_enemyPrefab, position, Quaternion.identity, _containerForEnemies);
        _enemies.Add(enemy);

        OnEnemyCreated?.Invoke(enemy);
        return enemy;
    }
}
