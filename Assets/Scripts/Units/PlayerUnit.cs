using System;
using UnityEngine;

public sealed class PlayerUnit : Unit, ITimerObserver
{
    [SerializeField] private EnemiesDetection _enemyDetection;
    [SerializeField] private BitsDetection _bitsDetection;

    public UnitWeapons Weapons { get; private set; }
    public Experience Experience { get; private set; }
    public UnitInventory Inventory { get; private set; }

    public event Action OnDodgedDamage;

    public override void Init()
    {
        base.Init();

        RegularSpeed = CurrentSpeed = 7;

        Experience = new();
        Experience.Init();

        Inventory = new();
        Inventory.OnAddedNewItem += OnAddedNewItem;
        Inventory.OnRemovedItem += OnRemovedItem;

        Weapons = new(this);

        _enemyDetection.Init();

        Health.SetMaxHealth(100, true);

        UpdateProperties();
    }

    public bool IsDodged()
    {
        int dodgePercent = (int) PlayerData.CalculatePropertieValue(
            PlayerData.Properties.Dodge,
            PlayerData.GetPropertieValue(PlayerData.Properties.Dodge)
        );

        bool isDodged = UnityEngine.Random.value <= dodgePercent / 100f;
        if (isDodged)
        {
            OnDodgedDamage?.Invoke();
        }

        return isDodged;
    }

    public void OnTimerReachedValue()
    {
        Health.Regenerate();
    }

    public void UpdateProperties()
    {
        UpdateMoveSpeed();
        UpdateMaxHealthValue(false);
        UpdateDistance();
    }

    protected override void DeInit()
    {
        Health.SetHealth(Health.MaxValue);
    }

    private void OnAddedNewItem(Item item)
    {
        if (item is Weapon weapon)
        {
            item.transform.parent = transform;
            item.transform.localPosition = Vector2.zero;
            Weapons.Equip(weapon);
        }
    }

    private void OnRemovedItem(Item item)
    {
        if (item is Weapon weapon)
        {
            Weapons.RemoveWeapon(weapon);
        }
    }

    private void UpdateMoveSpeed()
    {
        RegularSpeed = CurrentSpeed = PlayerData.CalculatePropertieValue(PlayerData.Properties.MoveSpeed, RegularSpeed);
    }

    private void UpdateMaxHealthValue(bool setHealthToMax)
    {
        int maxHealth = (int)PlayerData.CalculatePropertieValue(PlayerData.Properties.Health, Health.MaxValue);
        Health.SetMaxHealth(maxHealth, setHealthToMax);
    }

    private void UpdateDistance()
    {
        float newRadius = PlayerData.CalculatePropertieValue(PlayerData.Properties.Distance, _enemyDetection.CurrentRadius);
        _enemyDetection.SetDetectionRadius(newRadius);

        newRadius = PlayerData.CalculatePropertieValue(PlayerData.Properties.Distance, _bitsDetection.CurrentRadius);
        _bitsDetection.SetDetectionRadius(newRadius);
    }
}
