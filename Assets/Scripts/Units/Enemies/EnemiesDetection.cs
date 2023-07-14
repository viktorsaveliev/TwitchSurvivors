using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemiesDetection : MonoBehaviour
{
    [Inject] private readonly EnemyCounter _enemyDetection;

    private readonly List<Enemy> _closestEnemies = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Enemy enemy))
        {
            if(!_closestEnemies.Contains(enemy))
            {
                _closestEnemies.Add(enemy);
                _enemyDetection.UpdateEnemiesList(_closestEnemies);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            if (_closestEnemies.Contains(enemy))
            {
                _closestEnemies.Remove(enemy);
                _enemyDetection.UpdateEnemiesList(_closestEnemies);
            }
        }
    }
}
