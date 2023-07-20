using UnityEngine;
using Zenject;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemiesDetection : MonoBehaviour
{
    [Inject] private readonly EnemyCounter _enemyDetection;
    private CircleCollider2D _collider;
    
    public float CurrentRadius { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Enemy enemy))
        {
            _enemyDetection.AddEnemy(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            _enemyDetection.RemoveEnemy(enemy);
        }
    }

    public void Init()
    {
        _collider = GetComponent<CircleCollider2D>();
        SetDetectionRadius(8f);
    }

    public void SetDetectionRadius(float value)
    {
        if (value < 1 || value > 40) return;
        _collider.radius = value;
        CurrentRadius = value;
    }
}
