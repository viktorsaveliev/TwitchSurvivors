using System.Collections;
using UnityEngine;

public class Pistol : ShootableWeapon
{
    public override void Init()
    {
        Name = "Глок 19 \"JO-JO\"";
        BasicPrice = CurrentPrice = 50;

        SetCooldown(1f);
        SetDamage(6);
        SetBulletSpeed(30f);

        CurrentChargesCount = ChargesCount = 4;
        DelayBetweenShoots = 0.3f;

        CreateCharges(Damage);
    }

    public override void Improve()
    {
        if (ImprovementLevel >= MAX_IMPROVE_LEVEL) return;
        ImprovementLevel++;

        switch (ImprovementLevel)
        {
            case 1:
                SetDamage(10);
                CreateCharge(Damage, true);

                SetBulletSpeed(35f);
                break;

            case 2:
                DelayBetweenShoots = 0.2f;

                SetDamage(15);
                SetCooldown(1.5f);

                SetBulletSpeed(40f);
                break;

            case 3:
                DelayBetweenShoots = 0.1f;

                SetDamage(20);
                CreateCharge(Damage, true);

                SetBulletSpeed(45f);
                break;

            case 4:
                DelayBetweenShoots = 0.05f;

                SetCooldown(0.7f);
                SetDamage(30);
                CreateCharge(Damage, true);

                SetBulletSpeed(50f);
                break;
        }

        UpdatePrice();
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
            CurrentChargesCount--;
            LookAtTarget(Target);

            yield return delayBetweenShoots;
        }

        OnChargesEnded();
    }
}
