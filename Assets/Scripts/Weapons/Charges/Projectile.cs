using UnityEngine;

public class Projectile : EnemyBullet, IMoveable
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }

    public override void Init(int damage)
    {
        base.Init(damage);

        LifeTime = 4f;
        OnHitPlayer += OnLifeTimeEnded;
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

    protected override void OnLifeTimeEnded()
    {
        base.OnLifeTimeEnded();
        gameObject.SetActive(false);
    }
}
