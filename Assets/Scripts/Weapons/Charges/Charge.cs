using UnityEngine;

public abstract class Charge : MonoBehaviour
{
    public float Speed { get; protected set; }
    public float LifeTime { get; protected set; }
    public float CurrentLifeTime { get; protected set; }
    public int Damage { get; protected set; }

    private Vector2 _direction;

    private void FixedUpdate()
    {
        Vector2 movement = Speed * Time.fixedDeltaTime * _direction;
        transform.position -= new Vector3(movement.x, movement.y, 0);

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
            OnLifeTimeEnded();
        }
    }

    public virtual void Init()
    {
        gameObject.SetActive(false);
    }

    public void Shoot(Vector2 startPosition, Vector2 direction)
    {
        transform.parent = null;
        transform.position = startPosition;
        gameObject.SetActive(true);

        _direction = direction;
        CurrentLifeTime = Time.time + LifeTime;
    }

    protected virtual void OnLifeTimeEnded()
    {
        CurrentLifeTime = 0;
        gameObject.SetActive(false);
    }
}
