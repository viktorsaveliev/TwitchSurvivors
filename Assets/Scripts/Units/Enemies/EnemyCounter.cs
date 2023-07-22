using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyCounter : IEnemyCounter
{
    private readonly EnemyFactory _enemyFactory;

    private readonly List<Enemy> _enemiesInPlayerRange = new();

    private readonly List<Enemy> _sortedClosestEnemies = new();
    private readonly List<EnemyDistance> _enemyDistances = new();

    public EnemyCounter(EnemyFactory enemyFactory)
    {
        _enemyFactory = enemyFactory;
    }

    public void AddEnemy(Enemy enemy)
    {
        if (!_enemiesInPlayerRange.Contains(enemy))
        {
            _enemiesInPlayerRange.Add(enemy);
        }
    }

    public void RemoveEnemy(Enemy enemy)
    {
        if (_enemiesInPlayerRange.Contains(enemy))
        {
            _enemiesInPlayerRange.Remove(enemy);
        }
    }

    public List<Enemy> FindClosestEnemies(Vector2 targetPosition, int capacity)
    {
        _enemyDistances.Clear();
        _sortedClosestEnemies.Clear();

        foreach (Enemy enemy in _enemyFactory.Enemies)
        {
            if (!enemy.gameObject.activeSelf) continue;

            float distanceToTarget = Vector3.Distance(targetPosition, enemy.transform.position);
            _enemyDistances.Add(new EnemyDistance { Enemy = enemy, DistanceToTarget = distanceToTarget });
        }

        _enemyDistances.Sort((a, b) => a.DistanceToTarget.CompareTo(b.DistanceToTarget));

        int count = Mathf.Min(capacity, _enemyDistances.Count);

        for (int i = 0; i < count; i++)
        {
            _sortedClosestEnemies.Add(_enemyDistances[i].Enemy);
        }

        return _sortedClosestEnemies;
    }

    public Enemy[] GetAllEnemies() => _enemyFactory.Enemies.ToArray();

    public Transform GetClosestEnemy(Vector2 position)
    {
        if (_enemiesInPlayerRange == null || _enemiesInPlayerRange.Count < 1) return null;

        float closestDistance = float.MaxValue;
        Enemy closestEnemy = null;

        foreach (Enemy enemy in _enemiesInPlayerRange)
        {
            if (!enemy.gameObject.activeSelf) continue;

            float distance = Vector2.Distance(position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy == null ? null : closestEnemy.transform;
    }

    public Transform GetRandomEnemy()
    {
        if (_enemiesInPlayerRange == null || _enemiesInPlayerRange.Count < 1) return null;

        return _enemiesInPlayerRange[Random.Range(0, _enemiesInPlayerRange.Count)].transform;
    }
}

public class EnemyDistance
{
    public Enemy Enemy;
    public float DistanceToTarget;
}
