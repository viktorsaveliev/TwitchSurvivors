using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected GameObject ChargePrefab;

    protected List<Bullet> ChargesList = new();
    
    protected float Range;
    protected float Cooldown;
    protected int BulletsCount;

    public float CurrentCooldown { get; protected set; }
    public float DelayBetweenShoots { get; protected set; }
    public int CurrentChargesCount { get; protected set; }

    public abstract void Init();

    protected void CreateCharges(bool setParent = false)
    {
        for (int i = 0; i < BulletsCount; i++)
        {
            Bullet charge = Instantiate(ChargePrefab, setParent ? transform : null).GetComponent<Bullet>();
            charge.Init();

            ChargesList.Add(charge);
        }
    }

    public abstract void Shoot(IEnemyCounter enemyDetection);
}
