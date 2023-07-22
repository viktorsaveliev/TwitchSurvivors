using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bratik : Weapon, IChargesUser
{
    public event Action OnHitEnemy;

    protected Transform Target;
    
    private IEnemyCounter _enemyCounter;
    private List<Enemy> _closestEnemies = new();

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
            ShootOnNextTarget();

            OnHitEnemy?.Invoke();
        }
    }

    public void Shoot(IEnemyCounter enemyCounter)
    {
        if (CurrentCooldown > Time.time) return;

        IsActive = true;

        _enemyCounter = enemyCounter;

        if (_closestEnemies.Count == 0)
        {
            UpdateTargets();
        }

        ShootOnNextTarget();
        //Transform target = enemyDetection.GetClosestEnemy(transform.position);
    }

    protected void ShootBehaviour(IEnemyCounter enemyCounter)
    {
        
    }

    private void ShootOnNextTarget()
    {
        if (_closestEnemies.Count > 0 && CurrentChargesCount-- > 0)
        {
            SetNextTarget();

            if (Target == null || !Target.gameObject.activeSelf)
            {
                DisableCharge();
                return;
            }

            transform.DOMove(Target.position, MoveDurationToTarget);
            //Shoot(_enemyCounter);
        }
        else
        {
            DisableCharge();
        }
    }

    protected void SetNextTarget()
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

        ActivateCooldown();
        CurrentChargesCount = ChargesCount;
        IsActive = false;
    }
}
