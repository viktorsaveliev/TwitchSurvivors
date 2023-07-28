using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    [SerializeField] protected GameObject ChargePrefab;
    [SerializeField] protected string[] Descriptions = new string[5];

    protected const int MAX_IMPROVE_LEVEL = 4;
    protected List<Bullet> ChargesList = new();

    public int ChargesCount { get; protected set; }
    public float DelayBetweenShoots { get; protected set; }
    public int CurrentChargesCount { get; protected set; }
    public int ImprovementLevel { get; protected set; } = -1;
    public bool IsActive { get; protected set; }

    public int Damage { get; private set; }
    public float Cooldown { get; private set; }
    public float CurrentCooldown { get; private set; }

    public void SetDamage(int value)
    {
        if (value < 1 || value > 100) return;
        Damage = value;
    }
    
    public void SetCooldown(float value)
    {
        if (value < 0.01 || value > 60f) return;

        Cooldown = value;
        ActivateCooldown();
    }

    public void DestroyObject() => Destroy(gameObject);

    public override void Use()
    {
        if (ImprovementLevel < 0)
        {
            ImprovementLevel = 0;
        }
    }

    public override void UnUse()
    {
        ImprovementLevel = -1;
    }

    public abstract void Improve();
    
    public string GetDescriptionForNextLevel() => Descriptions[ImprovementLevel + 1];

    protected void UpdatePrice()
    {
        CurrentPrice = BasicPrice * (ImprovementLevel + 1);
    }

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
        ChargesCount = ChargesList.Count;
    }

    protected void ActivateCooldown()
    {
        float cooldown = PlayerData.CalculateValueWithPropertie(PlayerData.Properties.AttackSpeed, Cooldown, false);
        CurrentCooldown = Time.time + cooldown;
    }

    protected float GetCooldownValue()
    {
        float cooldown = PlayerData.CalculateValueWithPropertie(PlayerData.Properties.AttackSpeed, Cooldown);
        return cooldown;
    }

    protected int GetDamageValue()
    {
        int damage = (int) PlayerData.CalculateValueWithPropertie(PlayerData.Properties.Damage, Damage);

        int criticalDamagePercent = (int) PlayerData.CalculateValueWithPropertie(
            PlayerData.Properties.CriticalDamage, 
            PlayerData.GetPropertieValue(PlayerData.Properties.CriticalDamage)
        );

        if (UnityEngine.Random.value <= criticalDamagePercent / 100f)
        {
            damage *= 2;
        }

        return damage;
    }
}
