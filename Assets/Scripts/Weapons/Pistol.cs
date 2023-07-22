using System.Collections;
using UnityEngine;

public class Pistol : ShootableWeapon
{
    public override void Init()
    {
        Name = "Глок 19 \"JO-JO\"";
        Price = 500;

        SetCooldown(2f);
        SetDamage(5);
        SetBulletSpeed(30f);

        ChargesCount = 3;
        DelayBetweenShoots = 0.5f;

        CreateCharges(Damage);
    }

    public override void Improve()
    {
        if (ImprovementLevel > 4) return;
        ImprovementLevel++;

        switch (ImprovementLevel)
        {
            case 1:
                SetDamage(6);
                CreateCharge(Damage, true);

                SetBulletSpeed(40f);
                break;

            case 2:
                SetDamage(8);
                SetCooldown(2f);

                SetBulletSpeed(45f);
                break;

            case 3:
                DelayBetweenShoots = 0.2f;
                SetDamage(10);
                CreateCharge(Damage, true);

                SetBulletSpeed(50f);
                break;

            case 4:
                SetCooldown(1f);
                SetDamage(20);
                CreateCharge(Damage, true);

                SetBulletSpeed(60f);
                break;
        }

        UpdateChargesDamage();
    }

    protected override void SetNextTarget(IEnemyCounter enemyCounter)
    {
        Target = enemyCounter.GetClosestEnemy(transform.position);
    }

    protected override IEnumerator ShootBehaviour(IEnemyCounter enemyCounter)
    {
        WaitForSeconds delayBetweenShoots = new(DelayBetweenShoots);

        for (int i = 0; i < ChargesCount; i++)
        {
            if (ChargesList[i].gameObject.activeSelf) continue;

            SetNextTarget(enemyCounter);

            if (Target == null) break;

            ChargesList[i].Shoot(transform.position, Target, BulletSpeed);
            LookAtTarget(Target);

            yield return delayBetweenShoots;
        }

        OnChargesEnded();
    }
}
