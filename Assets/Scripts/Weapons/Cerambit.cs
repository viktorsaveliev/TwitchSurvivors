using System.Collections;
using UnityEngine;

public class Cerambit : Weapon, IChargesUser
{
    private readonly Vector2[] _stabbingPositions =
    {
        new Vector2(-1.03f, 0),
        new Vector2(1.03f, 0),
        new Vector2(0, 1.03f),
        new Vector2(0, -1.03f)
    };

    public override void Init()
    {
        Name = "Керамбит \"Fade\"";
        BasicPrice = CurrentPrice = 30;

        CurrentChargesCount = ChargesCount = 2;
        DelayBetweenShoots = 0.2f;
        
        SetCooldown(1);
        SetDamage(10);
        CreateCharges(Damage, true);
    }

    public override void Improve()
    {
        if (ImprovementLevel >= MAX_IMPROVE_LEVEL) return;
        ImprovementLevel++;

        switch (ImprovementLevel)
        {
            case 1:
                SetDamage(15);
                CreateCharge(Damage, true);
                break;

            case 2:
                SetDamage(20);
                break;

            case 3:
                DelayBetweenShoots = 0.1f;
                SetDamage(30);
                CreateCharge(Damage, true);
                break;

            case 4:
                SetCooldown(1f);
                SetDamage(40);
                break;
        }

        UpdatePrice();
        UpdateChargesDamage();
    }

    public void Shoot(IEnemyCounter enemyCounter)
    {
        if (CurrentCooldown > Time.time || IsActive || ChargesList.Count < 1) return;

        IsActive = true;
        StartCoroutine(ShootWithDelay());
    }

    private IEnumerator ShootWithDelay()
    {
        WaitForSeconds delayBetweenShoots = new(DelayBetweenShoots);

        for (int i = 0; i < ChargesCount; i++)
        {
            if (ChargesList[i].gameObject.activeSelf) continue;

            ChargesList[i].Shoot(_stabbingPositions[i], null, 0);
            yield return delayBetweenShoots;
        }

        IsActive = false;
        ActivateCooldown();
    }
}
