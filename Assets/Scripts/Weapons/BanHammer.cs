using System.Collections;
using UnityEngine;

public class BanHammer : Weapon, IChargesUser
{
    public override void Init()
    {
        Name = "Бан хаммер";
        BasicPrice = CurrentPrice = 40;

        SetCooldown(3f);
        SetDamage(15);

        CurrentChargesCount = ChargesCount = 2;
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
                CreateCharges(Damage);
                break;

            case 2:
                SetCooldown(2f);
                CreateCharge(Damage);
                break;

            case 3:
                CreateCharge(Damage);
                SetDamage(25);
                break;

            case 4:
                SetCooldown(1f);
                SetDamage(50);
                break;
        }

        UpdatePrice();
        UpdateChargesDamage();
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

        for(int i = 0; i < ChargesCount; i++)
        {
            Transform target = enemyCounter.GetRandomEnemy();

            if (target != null)
            {
                ChargesList[i].Shoot(target.position, target, 0);
            }

            yield return delayBetweenShoots;
        }

        CurrentChargesCount = ChargesCount;
        IsActive = false;
    }
}
