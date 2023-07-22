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
        Name = "Ðýï Ñêèëëçîðà";
        Price = 500;

        SetDamage(5);
        SetCooldown(0.8f);
    }

    public override void Improve()
    {
        if (ImprovementLevel > 3) return;
        ImprovementLevel++;

        switch (ImprovementLevel)
        {
            case 1:
                SetRadius(7f);
                SetDamage(8);
                break;

            case 2:
                SetRadius(9f);
                SetDamage(10);
                break;

            case 3:
                SetRadius(11f);
                SetDamage(15);
                break;

            case 4:
                SetRadius(13f);
                SetDamage(20);
                break;
        }
    }

    private void SetRadius(float value)
    {
        transform.localScale = new Vector2(value, value);
    }
}
