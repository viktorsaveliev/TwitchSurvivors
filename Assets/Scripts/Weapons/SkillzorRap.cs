using UnityEngine;

public class SkillzorRap : Weapon
{
    private readonly int _damage = 5;

    public override void Init()
    {
        Cooldown = 3f;
    }

    public override void Shoot(IEnemyCounter enemyDetection)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (CurrentCooldown > Time.time) return;

        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Health.TakeDamage(_damage);
            CurrentCooldown = Time.time + Cooldown;
        }
    }
}
