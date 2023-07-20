using UnityEngine;

public class RegularBullet : Bullet, IMoveable
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

    public override void Shoot(Vector2 startPosition, Vector2 direction, float speed)
    {
        base.Shoot(startPosition, direction, speed);

        transform.parent = null;
        transform.position = startPosition;
        Direction = direction;
    }

    protected override void OnLifeTimeEnded()
    {
        base.OnLifeTimeEnded();
        gameObject.SetActive(false);
    }

    public void Move()
    {
        Vector2 movement = Speed * Time.fixedDeltaTime * Direction;
        transform.position -= new Vector3(movement.x, movement.y, 0);
    }
}
