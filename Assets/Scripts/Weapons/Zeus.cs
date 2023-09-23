using System.Collections;
using System.Collections.Generic;

public class Zeus : ShootableWeapon
{
    private List<Enemy> _closestEnemies = new();
    private IEnemyCounter _enemyCounter;

    public override void Improve()
    {
        if (ImprovementLevel >= MAX_IMPROVE_LEVEL) return;
        ImprovementLevel++;

        switch (ImprovementLevel)
        {
            case 1:
                ChargesCount = 4;
                break;

            case 2:
                SetCooldown(3f);
                break;

            case 3:
                SetDamage(25);
                break;

            case 4:
                ChargesCount = 6;
                SetCooldown(2f);
                break;
        }

        UpdatePrice();
        UpdateChargesDamage();
    }

    public override void Init()
    {
        Name = "Молния Зевса";
        BasicPrice = CurrentPrice = 35;

        SetCooldown(3);
        SetDamage(10);
        SetBulletSpeed(20f);

        CreateCharge(Damage);
        CurrentChargesCount = ChargesCount = 2;

        PlayerBullet bullet = (PlayerBullet) ChargesList[0];
        bullet.OnHitEnemy += ShootOnNextTarget;
        bullet.LifeTimeEnded += OnChargesEnded;
    }

    protected override IEnumerator ShootBehaviour(IEnemyCounter enemyCounter)
    {
        _enemyCounter = enemyCounter;

        ChargesList[0].transform.position = transform.position;

        if (_closestEnemies.Count == 0)
        {
            UpdateTargets();
        }

        ShootOnNextTarget(null);

        yield break;
    }

    private void ShootOnNextTarget(Enemy enemy)
    {
        if (_closestEnemies.Count > 0 && CurrentChargesCount-- > 0)
        {
            SetNextTarget(_enemyCounter);

            if (Target == null || !Target.gameObject.activeSelf)
            {
                DisableCharge();
                return;
            }

            SoundFX.Play();
            ChargesList[0].Shoot(transform.position, Target, BulletSpeed);
        }
        else
        {
            DisableCharge();
        }
    }

    protected override void SetNextTarget(IEnemyCounter enemyCounter)
    {
        if (_closestEnemies.Count > 0)
        {
            Target = _closestEnemies[0].transform;
            _closestEnemies.Remove(_closestEnemies[0]);
        }
        else
        {
            Target = null;
        }
    }

    private void UpdateTargets()
    {
        _closestEnemies = _enemyCounter.FindClosestEnemies(transform.position, ChargesCount);
    }

    private void DisableCharge()
    {
        _closestEnemies.Clear();
        ChargesList[0].gameObject.SetActive(false);
        OnChargesEnded();
    }
}
