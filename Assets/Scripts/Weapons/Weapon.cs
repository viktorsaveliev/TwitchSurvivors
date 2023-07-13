using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected GameObject ChargePrefab;

    protected List<Charge> ChargesList = new();
    
    protected float Range;
    protected float Cooldown;

    protected int ChargesCount;

    public float Distance { get; protected set; }
    public float CurrentCooldown { get; protected set; }
    public float DelayBetweenShoots { get; protected set; }
    public int CurrentChargesCount { get; protected set; }

    public abstract void Init();

    public void Shoot(IEnemyDetection enemyDetection)
    {
        if (Time.time < CurrentCooldown) return;

        StartCoroutine(ShootWithDelay(enemyDetection));

        CurrentCooldown = Time.time + Cooldown;
    }

    protected void CreateCharges()
    {
        for (int i = 0; i < ChargesCount; i++)
        {
            Charge charge = Instantiate(ChargePrefab).GetComponent<Charge>();
            charge.Init();

            ChargesList.Add(charge);
        }
    }

    private IEnumerator ShootWithDelay(IEnemyDetection enemyDetection)
    {
        if (ChargesList.Count < 1) 
            yield break;

        WaitForSeconds delayBetweenShoots = new(DelayBetweenShoots);

        foreach (Charge charge in ChargesList)
        {
            if (charge.gameObject.activeSelf) continue;

            Vector2 enemyPosition = enemyDetection.GetClosestEnemyPosition(transform.position);

            if (GetDistanceTo(enemyPosition) < Distance)
            {
                Vector2 direction = (Vector2)transform.position - enemyPosition;
                direction.Normalize();

                LookAtTarget(direction);

                charge.Shoot(transform.position, direction);
            }

            if(--CurrentChargesCount <= 0)
            {
                OnChargesEnded();
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

    private float GetDistanceTo(Vector2 position)
    {
        return Vector2.Distance(transform.position, position);
    }

    private void OnChargesEnded()
    {
        CurrentChargesCount = ChargesCount;
    }
}
