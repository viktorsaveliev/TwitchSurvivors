using UnityEngine;

public class RegularBullet : PlayerBullet, IMoveable
{
    private int _penetration;
    private int _baseDamage;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }

    public override void Init(int damage)
    {
        base.Init(damage);

        _baseDamage = Damage;
        _penetration = 1;
        Speed = 30f;
        LifeTime = 0.8f;

        OnHitEnemy += OnHit;
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

    public void Move()
    {
        Vector3 movement = Speed * Time.fixedDeltaTime * Direction;
        transform.position -= movement;
    }

    protected override void OnLifeTimeEnded()
    {
        base.OnLifeTimeEnded();

        Damage = _baseDamage;
        _penetration = 1;
        gameObject.SetActive(false);
    }

    private void OnHit()
    {
        if (--_penetration == 0)
        {
            Damage /= 2;
        }
        else
        {
            OnLifeTimeEnded();
        }
    }

}
