using UnityEngine;

public class ZeusLighting : Bullet, IMoveable
{
    public Transform LastHittedTarget { get; private set; }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }

    public override void Init(int damage)
    {
        base.Init(damage);

        Speed = 30f;
        LifeTime = 3f;

        OnHitEnemy += () => LastHittedTarget = Target;
    }

    public override void Shoot(Vector2 startPosition, Transform target, float speed)
    {
        base.Shoot(startPosition, target, speed);

        transform.parent = null;

        LookAtTarget(target);
    }

    public void Move()
    {
        Direction = (Vector2)(transform.position - Target.position);
        Direction.Normalize();

        Vector3 movement = Speed * Time.fixedDeltaTime * Direction;
        transform.position -= movement;
    }

    protected override void OnLifeTimeEnded()
    {
        base.OnLifeTimeEnded();
        gameObject.SetActive(false);
    }
}
