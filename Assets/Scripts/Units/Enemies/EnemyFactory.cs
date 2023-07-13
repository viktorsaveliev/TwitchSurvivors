using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private RegularEnemy _enemyPrefab;
    [SerializeField] private Transform _containerForEnemies;

    private readonly List<Enemy> _enemies = new();
    public IReadOnlyList<Enemy> GetAllEnemies => _enemies;

    public RegularEnemy CreateRegularEnemy(Vector3 position)
    {
        RegularEnemy enemy = Instantiate(_enemyPrefab, position, Quaternion.identity, _containerForEnemies);
        _enemies.Add(enemy);

        return enemy;
    }
}
