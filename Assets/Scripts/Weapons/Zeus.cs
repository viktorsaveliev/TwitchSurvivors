using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeus : ShootableWeapon
{
    private List<Enemy> _closestEnemies = new();
    private IEnemyCounter _enemyCounter;

    public override void Improve()
    {
        throw new System.NotImplementedException();
    }

    public override void Init()
    {
        ItemName = "Молнии Олимпуса";
        Price = 500;

        SetCooldown(6);
        SetDamage(10);
        SetBulletSpeed(20f);

        CurrentChargesCount = ChargesCount = 5;
        CreateCharge(Damage);

        ChargesList[0].OnHitEnemy += MoveToNextTarget;
        ChargesList[0].LifeTimeEnded += OnChargesEnded;
    }

    protected override IEnumerator ShootBehaviour(IEnemyCounter enemyCounter)
    {
        _enemyCounter = enemyCounter;

        ChargesList[0].transform.position = transform.position;
        MoveToNextTarget();

        yield break;
    }

    private void MoveToNextTarget()
    {
        if (_closestEnemies.Count == 0)
        {
            UpdateTargets();
        }

        if (CurrentChargesCount-- > 0)
        {
            SetNextTarget(_enemyCounter);

            if (Target == null)
            {
                ChargesList[0].gameObject.SetActive(false);
                OnChargesEnded();
                return;
            }

            Vector2 direction = (Vector2)ChargesList[0].transform.position - (Vector2) Target.position;
            direction.Normalize();

            ChargesList[0].Shoot(transform.position, direction, BulletSpeed);
            ChargesList[0].SetTarget(Target);
        }
        else
        {
            ChargesList[0].gameObject.SetActive(false);
            OnChargesEnded();
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
        if (_closestEnemies.Count > 0) return;
        _closestEnemies = _enemyCounter.FindClosestEnemies(transform.position, ChargesCount);
    }
}
