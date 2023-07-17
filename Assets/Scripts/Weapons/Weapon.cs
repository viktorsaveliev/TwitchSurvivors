using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected GameObject ChargePrefab;

    protected List<Bullet> ChargesList = new();
    
    protected float Range;
    protected int ChargesCount;

    private float _cooldown;
    
    public int Damage { get; private set; }
    public float CurrentCooldown { get; private set; }
    public float DelayBetweenShoots { get; protected set; }
    public int CurrentChargesCount { get; protected set; }
    public int ImprovementLevel { get; protected set; }
    public bool IsActive { get; protected set; }

    public void SetDamage(int value)
    {
        if (value < 1 || value > 100) return;
        Damage = value;
    }
    
    public void SetCooldown(float value)
    {
        if (value < 0.01 || value > 60f) return;
        _cooldown = value;
    }

    public abstract void Init();

    protected abstract void Improve();

    protected void CreateCharges(int damage, bool setParent = false)
    {
        for (int i = 0; i < ChargesCount; i++)
        {
            Bullet charge = Instantiate(ChargePrefab, setParent ? transform : null).GetComponent<Bullet>();
            charge.Init(damage);

            ChargesList.Add(charge);
        }
    }

    protected void UpdateChargesDamage()
    {
        for (int i = 0; i < ChargesList.Count; i++)
        {
            ChargesList[i].SetDamage(Damage);
        }
    }

    protected void CreateCharge(int damage, bool setParent = false)
    {
        Bullet charge = Instantiate(ChargePrefab, setParent ? transform : null).GetComponent<Bullet>();
        charge.Init(damage);

        ChargesList.Add(charge);
    }

    protected void ActivateCooldown()
    {
        float cooldown = PlayerData.CalculatePropertieValue(PlayerData.Properties.AttackSpeed, _cooldown, false);
        CurrentCooldown = Time.time + cooldown;
    }

    protected float GetCooldownValue()
    {
        float cooldown = PlayerData.CalculatePropertieValue(PlayerData.Properties.AttackSpeed, _cooldown);
        return cooldown;
    }

    protected int GetDamageValue()
    {
        int damage = (int) PlayerData.CalculatePropertieValue(PlayerData.Properties.Damage, Damage);

        int criticalDamagePercent = (int) PlayerData.CalculatePropertieValue(
            PlayerData.Properties.CriticalDamage, 
            PlayerData.GetPropertieValue(PlayerData.Properties.CriticalDamage)
        );

        if (Random.value <= criticalDamagePercent / 100f)
        {
            damage *= 2;
        }

        return damage;
    }
}
