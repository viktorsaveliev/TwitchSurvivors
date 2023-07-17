using DG.Tweening;
using UnityEngine;

public class FirstBratik : Weapon, IChargesUser
{
    private readonly float _speed = 0.4f;

    public override void Init()
    {
        SetCooldown(3);
        SetDamage(25);

        CurrentChargesCount = ChargesCount = 2;
        transform.parent = null;
    }

    public void Shoot(IEnemyCounter enemyDetection)
    {
        if (CurrentCooldown > Time.time) return;

        IsActive = true;

        Vector2 position = enemyDetection.GetClosestEnemyPosition(transform.position);
        transform.DOMove(position, _speed).OnComplete(() 
            => IsActive = false);
    }

    protected override void Improve()
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
