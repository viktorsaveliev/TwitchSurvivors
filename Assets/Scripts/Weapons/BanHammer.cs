using System.Collections;
using UnityEngine;

public class BanHammer : Weapon, IChargesUser
{
    public override void Improve()
    {
        if (ImprovementLevel > 4) return;
        ImprovementLevel++;

        switch (ImprovementLevel)
        {
            case 1:
                CreateCharges(Damage);
                break;

            case 2:
                SetCooldown(3f);
                CreateCharge(Damage);
                break;

            case 3:
                CreateCharge(Damage);
                SetDamage(25);
                break;

            case 4:
                SetDamage(50);
                break;
        }

        UpdateChargesDamage();
    }

    public override void Init()
    {
        Name = "Бан хаммер";
        Price = 500;

        SetCooldown(5);
        SetDamage(15);

        CurrentChargesCount = ChargesCount = 2;
        DelayBetweenShoots = 0.3f;

        CreateCharges(Damage);
    }

    public void Shoot(IEnemyCounter enemyCounter)
    {
        if (CurrentCooldown > Time.time || IsActive) return;

        IsActive = true;
        StartCoroutine(ShootWithDelay(enemyCounter));

        ActivateCooldown();
    }

    private IEnumerator ShootWithDelay(IEnemyCounter enemyCounter)
    {
        if (ChargesList.Count < 1) yield break;

        WaitForSeconds delayBetweenShoots = new(DelayBetweenShoots);

        foreach (Bullet charge in ChargesList)
        {
            Transform target = enemyCounter.GetRandomEnemy();

            if (target != null)
            {
                charge.Shoot(target.position, target, 0);
            }

            yield return delayBetweenShoots;
        }

        CurrentChargesCount = ChargesCount;
        IsActive = false;
    }
}
