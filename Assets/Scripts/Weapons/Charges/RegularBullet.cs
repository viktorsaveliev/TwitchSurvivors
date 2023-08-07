using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class RegularBullet : PlayerBullet, IMoveable
{
    private TrailRenderer _trail;
    private int _penetration;

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

        BaseDamage = Damage;
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

        _trail.Clear();
        _trail.emitting = true;
    }

    public void Move()
    {
        Vector3 movement = Speed * Time.fixedDeltaTime * Direction;
        transform.position -= movement;
    }

    protected override void OnLifeTimeEnded()
    {
        base.OnLifeTimeEnded();

        Damage = BaseDamage;
        _penetration = 1;

        _trail.emitting = false;
        gameObject.SetActive(false);
    }

    private void OnHit(Enemy enemy)
    {
        if (enemy.EnemyLevel < 1 && --_penetration == 0)
        {
            Damage /= 2;
        }
        else
        {
            OnLifeTimeEnded();
        }
    }

}
