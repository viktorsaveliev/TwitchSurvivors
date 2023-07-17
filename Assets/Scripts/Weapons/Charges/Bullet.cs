using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public abstract class Bullet : MonoBehaviour
{
    public float Speed { get; protected set; }
    public float LifeTime { get; protected set; }
    public float CurrentLifeTime { get; protected set; }
    public int Damage { get; private set; }

    protected Vector2 Direction;

    protected virtual void FixedUpdate()
    {
        if(CurrentLifeTime <= Time.time)
        {
            OnLifeTimeEnded();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            int damage = (int)PlayerData.CalculatePropertieValue(PlayerData.Properties.Damage, Damage);
            enemy.Health.TakeDamage(damage);
            OnHitEnemy();
        }
    }

    public void SetDamage(int value)
    {
        Damage = value;
    }

    public virtual void Init(int damage)
    {
        SetDamage(damage);
        gameObject.SetActive(false);
    }

    public virtual void Shoot(Vector2 startPosition, Vector2 direction)
    {
        gameObject.SetActive(true);
        CurrentLifeTime = Time.time + LifeTime;
    }

    protected virtual void OnLifeTimeEnded()
    {
        CurrentLifeTime = 0;
    }

    protected abstract void OnHitEnemy();
}
