using System.Collections;
using UnityEngine;

public class WeaponWithBullets : Weapon
{
    public override void Init()
    {
        
    }

    public override void Shoot(IEnemyCounter enemyDetection)
    {
        if (Time.time < CurrentCooldown || enemyDetection.GetClosestEnemyPosition(transform.position) == Vector2.zero) return;

        StartCoroutine(ShootWithDelay(enemyDetection));

        CurrentCooldown = Time.time + Cooldown;
    }

    private IEnumerator ShootWithDelay(IEnemyCounter enemyCounter)
    {
        if (ChargesList.Count < 1) yield break;

        WaitForSeconds delayBetweenShoots = new(DelayBetweenShoots);
        foreach (Bullet charge in ChargesList)
        {
            if (charge.gameObject.activeSelf) continue;

            Vector2 enemyPosition = enemyCounter.GetClosestEnemyPosition(transform.position);

            if (enemyPosition == Vector2.zero) break;

            Vector2 direction = (Vector2)transform.position - enemyPosition;

            direction.Normalize();
            LookAtTarget(direction);

            charge.Shoot(transform.position, direction);

            if (--CurrentChargesCount <= 0)
            {
                OnBulletsEnded();
                break;
            }
            else
            {
                yield return delayBetweenShoots;
            }
        }
    }

    private void LookAtTarget(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnBulletsEnded()
    {
        CurrentChargesCount = BulletsCount;
    }
}
