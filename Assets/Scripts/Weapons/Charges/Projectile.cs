using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class Projectile : EnemyBullet, IMoveable
{
    private TrailRenderer _trail;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }

    public override void Init(int damage)
    {
        base.Init(damage);

        _trail = GetComponent<TrailRenderer>();
        _trail.emitting = false;

        LifeTime = 4f;
        OnHitPlayer += OnPlayerHitted;
    }

    public override void Shoot(Vector2 startPosition, Transform target, float speed)
    {
        base.Shoot(startPosition, target, speed);

        transform.parent = null;
        transform.position = startPosition;

        if (target != null)
        {
            Vector2 direction = (Vector2)(transform.position - target.position);
            direction.Normalize();

            Direction = direction;
        }

        _trail.Clear();
        _trail.emitting = true;
    }

    public void Move()
    {
        Vector3 movement = Speed * Time.fixedDeltaTime * Direction;
        transform.position -= movement;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    private void OnPlayerHitted(PlayerUnit unit)
    {
        OnLifeTimeEnded();
    }

    protected override void OnLifeTimeEnded()
    {
        base.OnLifeTimeEnded();

        _trail.emitting = false;
        gameObject.SetActive(false);
    }
}
