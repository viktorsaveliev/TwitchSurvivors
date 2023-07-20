using System.Collections;
using UnityEngine;

public abstract class ShootableWeapon : Weapon, IChargesUser
{
    protected float BulletSpeed;
    protected Transform Target;
    
    public virtual void Shoot(IEnemyCounter enemyCounter)
    {
        if (CurrentCooldown > Time.time || IsActive || ChargesList.Count < 1) return;

        IsActive = true;

        StartCoroutine(ShootBehaviour(enemyCounter));
        ActivateCooldown();
    }

    protected void SetBulletSpeed(float speed)
    {
        if (speed < 1 || speed > 100) return;

        BulletSpeed = speed;
    }

    protected abstract void SetNextTarget(IEnemyCounter enemyCounter);

    protected abstract IEnumerator ShootBehaviour(IEnemyCounter enemyCounter);

    protected void LookAtTarget(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    protected void OnChargesEnded()
    {
        CurrentChargesCount = ChargesCount;
        IsActive = false;
    }
}
