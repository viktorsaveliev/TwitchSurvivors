using System;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float Speed { get; protected set; }
    public float LifeTime { get; protected set; }
    public float CurrentLifeTime { get; protected set; }
    public int Damage { get; private set; }

    public event Action LifeTimeEnded;

    protected Vector2 Direction;

    protected Transform Target;

    private bool _isLifeTimeEnded;

    protected virtual void FixedUpdate()
    {
        if(CurrentLifeTime <= Time.time && !_isLifeTimeEnded)
        {
            OnLifeTimeEnded();
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

    public virtual void Shoot(Vector2 startPosition, Transform target, float speed)
    {
        _isLifeTimeEnded = false;

        Target = target;
        Speed = speed;

        gameObject.SetActive(true);

        CurrentLifeTime = Time.time + LifeTime;
    }

    protected virtual void OnLifeTimeEnded()
    {
        _isLifeTimeEnded = true;
        CurrentLifeTime = 0;
        LifeTimeEnded?.Invoke();
    }

    protected void LookAtTarget(Transform target)
    {
        Vector2 direction = (Vector2) (transform.position - target.position);
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
