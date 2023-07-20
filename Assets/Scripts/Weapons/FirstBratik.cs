using DG.Tweening;
using UnityEngine;

public class FirstBratik : Weapon, IChargesUser
{
    private readonly float _speed = 0.4f;

    private void OnEnable()
    {
        transform.parent = null;
    }

    public override void Init()
    {
        ItemName = "Первый Братик";
        Price = 500;

        SetCooldown(3);
        SetDamage(25);

        CurrentChargesCount = ChargesCount = 2;
    }

    public void Shoot(IEnemyCounter enemyDetection)
    {
        if (CurrentCooldown > Time.time) return;

        IsActive = true;

        Transform target = enemyDetection.GetClosestEnemy(transform.position);

        if (target != null)
        {
            transform.DOMove(target.position, _speed).OnComplete(()
            => IsActive = false);
        }
    }

    public override void Improve()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CurrentCooldown > Time.time) return;

        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Health.TakeDamage(GetDamageValue());
            if (--CurrentChargesCount <= 0)
            {
                ActivateCooldown();
                CurrentChargesCount = ChargesCount;
            }
        }
    }
}
