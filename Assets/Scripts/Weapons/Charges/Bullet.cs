using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float Speed { get; protected set; }
    public float LifeTime { get; protected set; }
    public float CurrentLifeTime { get; protected set; }
    public int Damage { get; protected set; }

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
            enemy.Health.TakeDamage(Damage);
            OnHitEnemy();
        }
    }

    public virtual void Init()
    {
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
