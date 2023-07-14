using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : IEnemyCounter
{
    private IReadOnlyList<Enemy> _enemies;

    public void UpdateEnemiesList(IReadOnlyList<Enemy> enemies)
    {
        _enemies = enemies;
    }

    public Vector2 GetClosestEnemyPosition(Vector2 position)
    {
        if (_enemies == null || _enemies.Count < 1) return Vector2.zero;

        float closestDistance = float.MaxValue;
        Enemy closestEnemy = null;

        foreach (Enemy enemy in _enemies)
        {
            if (!enemy.gameObject.activeSelf) continue;

            float distance = Vector2.Distance(position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy == null ? Vector2.zero : closestEnemy.transform.position;
    }
}
