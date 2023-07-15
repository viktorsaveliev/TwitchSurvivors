using DG.Tweening;
using UnityEngine;

public class FirstBratik : Weapon, IAttackable, IChargesUser
{
    private readonly float _speed = 0.4f;

    public int Damage { get; set; }

    public override void Init()
    {
        Cooldown = 3;
        Damage = 25;
        CurrentChargesCount = ChargesCount = 2;

        transform.parent = null;
    }

    public void SetDamage(int value)
    {
        if (value < 1 || value > 100) return;
        Damage = value;
    }

    public void Shoot(IEnemyCounter enemyDetection)
    {
        if (CurrentCooldown > Time.time) return;

        IsActive = true;

        Vector2 position = enemyDetection.GetClosestEnemyPosition(transform.position);
        transform.DOMove(position, _speed).OnComplete(() 
            => IsActive = false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CurrentCooldown > Time.time) return;

        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Health.TakeDamage(Damage);
            if(--CurrentChargesCount <= 0)
            {
                CurrentCooldown = Time.time + Cooldown;
                CurrentChargesCount = ChargesCount;
            }
        }
    }
}
