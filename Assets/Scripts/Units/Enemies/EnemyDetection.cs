using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : IEnemyDetection
{
    private readonly IReadOnlyList<Enemy> _enemies;

    public EnemyDetection(IReadOnlyList<Enemy> enemies)
    {
        _enemies = enemies;
    }

    public Vector2 GetClosestEnemyPosition(Vector2 position)
    {
        float closestDistance = float.MaxValue;
        Enemy closestEnemy = null;

        foreach (Enemy enemy in _enemies)
        {
            float distance = Vector2.Distance(position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy.transform.position;
    }
}
