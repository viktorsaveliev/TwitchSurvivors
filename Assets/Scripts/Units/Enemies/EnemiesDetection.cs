using UnityEngine;
using Zenject;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemiesDetection : MonoBehaviour
{
    [Inject] private readonly EnemyCounter _enemyDetection;

    public readonly float OriginalRadius = 8f;
    private CircleCollider2D _collider;
    
    public float CurrentRadius { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Enemy enemy))
        {
            Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            Remove(enemy);
        }
    }

    public void Init()
    {
        _collider = GetComponent<CircleCollider2D>();
        SetDetectionRadius(OriginalRadius);
    }

    public void SetDetectionRadius(float value)
    {
        if (value < 1 || value > 40) return;
        _collider.radius = value;
        CurrentRadius = value;
    }

    private void Add(Enemy enemy)
    {
        _enemyDetection.AddEnemy(enemy);
        enemy.OnDead += Remove;
    }

    private void Remove(Enemy enemy)
    {
        _enemyDetection.RemoveEnemy(enemy);
        enemy.OnDead -= Remove;
    }
}
