using System;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float Speed { get; protected set; }
    public float LifeTime { get; protected set; }
    public float CurrentLifeTime { get; protected set; }
    public int Damage { get; private set; }

    public event Action OnHitEnemy;
    public event Action LifeTimeEnded;

    protected Vector2 Direction;

    protected Transform Target;

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

            OnHitEnemy?.Invoke();
        }
    }

    public void SetTarget(Transform target)
    {
        Target = target;
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

    public virtual void Shoot(Vector2 startPosition, Vector2 direction, float speed)
    {
        Speed = speed;
        gameObject.SetActive(true);
        CurrentLifeTime = Time.time + LifeTime;
    }

    protected virtual void OnLifeTimeEnded()
    {
        CurrentLifeTime = 0;
        LifeTimeEnded?.Invoke();
    }

    protected void LookAtTarget(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
