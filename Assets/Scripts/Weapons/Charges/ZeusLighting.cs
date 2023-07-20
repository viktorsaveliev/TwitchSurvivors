using UnityEngine;

public class ZeusLighting : Bullet, IMoveable
{
    private Vector2 _startPos;
    private float _elapsedTime;

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
    }

    public override void Shoot(Vector2 startPosition, Vector2 direction, float speed)
    {
        base.Shoot(startPosition, direction, speed);

        _startPos = transform.position;
        transform.parent = null;
        Direction = direction;

        LookAtTarget(direction);
    }

    protected override void OnLifeTimeEnded()
    {
        base.OnLifeTimeEnded();
        gameObject.SetActive(false);
    }

    public void Move()
    {
        Direction = (Vector2)(transform.position - Target.position);
        Direction.Normalize();

        Vector2 movement = Speed * Time.fixedDeltaTime * Direction;
        transform.position -= new Vector3(movement.x, movement.y, 0);
    }
}
