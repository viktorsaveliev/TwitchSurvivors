using DG.Tweening;
using System;
using UnityEngine;

public abstract class Bratik : Weapon, IChargesUser
{
    public event Action OnHitEnemy;

    protected Transform Target;
    
    private IEnemyCounter _enemyCounter;

    public float MoveDurationToTarget { get; protected set; } = 0.4f;

    private void OnEnable()
    {
        transform.parent = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CurrentCooldown > Time.time) return;

        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Health.TakeDamage(GetDamageValue());
            HitTarget();

            OnHitEnemy?.Invoke();
        }
    }

    public void Shoot(IEnemyCounter enemyCounter)
    {
        if (CurrentCooldown > Time.time || IsActive) return;

        IsActive = true;

        _enemyCounter = enemyCounter;

        HitTarget();
    }

    private void HitTarget()
    {
        if (CurrentChargesCount-- > 0)
        {
            UpdateTargets();

            if (Target != null)
            {
                transform.DOMove(Target.position, MoveDurationToTarget);
            }
        }
        else
        {
            DisableCharge();
        }
    }

    private void UpdateTargets()
    {
        if (_enemyCounter == null) return;

        Target = _enemyCounter.GetRandomEnemy();
    }

    private void DisableCharge()
    {
        ActivateCooldown();

        CurrentChargesCount = ChargesCount;
        IsActive = false;
    }
}
