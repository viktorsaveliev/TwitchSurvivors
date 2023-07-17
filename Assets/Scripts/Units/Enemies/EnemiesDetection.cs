using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemiesDetection : MonoBehaviour
{
    [Inject] private readonly EnemyCounter _enemyDetection;

    private readonly List<Enemy> _closestEnemies = new();
    private CircleCollider2D _collider;

    public float CurrentRadius { get; private set; }

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

    public void Init()
    {
        _collider = GetComponent<CircleCollider2D>();
        UpdateRadius(11f);
    }

    public void UpdateRadius(float value)
    {
        _collider.radius = value;
        CurrentRadius = value;
    }
}
