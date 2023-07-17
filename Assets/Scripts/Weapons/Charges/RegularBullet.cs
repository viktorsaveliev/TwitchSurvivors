using UnityEngine;

public class RegularBullet : Bullet
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Vector2 movement = Speed * Time.fixedDeltaTime * Direction;
        transform.position -= new Vector3(movement.x, movement.y, 0);
    }

    public override void Init(int damage)
    {
        base.Init(damage);

        Speed = 30f;
        LifeTime = 0.8f;
    }

    public override void Shoot(Vector2 startPosition, Vector2 direction)
    {
        base.Shoot(startPosition, direction);

        transform.parent = null;
        transform.position = startPosition;
        Direction = direction;
    }

    protected override void OnHitEnemy()
    {
        OnLifeTimeEnded();
    }

    protected override void OnLifeTimeEnded()
    {
        base.OnLifeTimeEnded();
        gameObject.SetActive(false);
    }
}
