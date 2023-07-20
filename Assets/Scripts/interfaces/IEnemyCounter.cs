using System.Collections.Generic;
using UnityEngine;

public interface IEnemyCounter
{
    public List<Enemy> FindClosestEnemies(Vector2 targetPosition, int capacity);
    public Transform GetClosestEnemy(Vector2 position);
    public Transform GetRandomEnemy();
}
