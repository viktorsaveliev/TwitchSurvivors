using UnityEngine;

public class SkillzorRap : Weapon, IAttackable
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            if (enemy.DamageImmunity > Time.time) return;

            enemy.Health.TakeDamage(GetDamageValue());
            enemy.DamageImmunity = Time.time + GetCooldownValue();
        }
    }

    public override void Init()
    {
        SetDamage(5);
        SetCooldown(0.8f);
    }

    protected override void Improve()
    {
        throw new System.NotImplementedException();
    }
}
