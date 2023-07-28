using UnityEngine;

public class RegularBullet : PlayerBullet, IMoveable
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }

    public override void Init(int damage)
    {
        base.Init(damage);

        Speed = 30f;
        LifeTime = 0.8f;

        OnHitEnemy += OnLifeTimeEnded;
    }

    public override void Shoot(Vector2 startPosition, Transform target, float speed)
    {
        base.Shoot(startPosition, target, speed);

        transform.parent = null;
        transform.position = startPosition;

        Vector2 direction = (Vector2)(transform.position - target.position);
        direction.Normalize();

        Direction = direction;
    }

    protected override void OnLifeTimeEnded()
    {
        base.OnLifeTimeEnded();
        gameObject.SetActive(false);
    }

    public void Move()
    {
        Vector3 movement = Speed * Time.fixedDeltaTime * Direction;
        transform.position -= movement;
    }
}
