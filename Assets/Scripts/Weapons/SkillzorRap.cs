using UnityEngine;

public class SkillzorRap : Weapon, IAttackable
{
    public int Damage { get; set; }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (CurrentCooldown > Time.time) return;

        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Health.TakeDamage(Damage);
            CurrentCooldown = Time.time + Cooldown;
        }
    }

    public override void Init()
    {
        Damage = 5;
        Cooldown = 1f;
    }

    public void SetDamage(int value)
    {
        if (value < 1 || value > 100) return;
        Damage = value;
    }
}
