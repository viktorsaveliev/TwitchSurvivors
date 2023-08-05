using System;
using UnityEngine;
using Zenject;

public sealed class PlayerUnit : Unit, ITimerObserver
{
    [Inject] private readonly Notify _notify;

    [SerializeField] private EnemiesDetection _enemyDetection;
    [SerializeField] private BitsDetection _bitsDetection;
    [SerializeField] private SpriteRenderer _head;
    [SerializeField] private Transform[] _weaponPositions;

    public UnitWeapons Weapons { get; private set; }
    public Experience Experience { get; private set; }
    public UnitInventory Inventory { get; private set; }

    public event Action OnDodgedDamage;
    public event Action OnPickupBits;

    public string LastHittedEnemyNickname = string.Empty;

    public override void Init()
    {
        base.Init();

        _head.sprite = PlayerData.SelectedCharacter.IconLeft;

        OriginalSpeed = RegularSpeed = CurrentSpeed = 7;

        Experience = new();
        Experience.Init();

        Inventory = new();
        Inventory.OnAddedNewItem += OnAddedNewItem;
        Inventory.OnRemovedItem += OnRemovedItem;

        Weapons = new(this, _weaponPositions);
        Weapons.OnWeaponsHiden += OnWeaponsHiden;

        _enemyDetection.Init();
        _bitsDetection.Init();

        Health.SetMaxHealth(100, true);
    }

    public override void Move(Vector2 direction)
    {
        base.Move(direction);

        if (direction.x > 0)
        {
            _head.sprite = PlayerData.SelectedCharacter.IconRight;
        }
        else
        {
            _head.sprite = PlayerData.SelectedCharacter.IconLeft;
        }

        Animator.SetFloat("Speed", Rigidbody.velocity.magnitude);
    }

    public void PickupBits()
    {
        OnPickupBits?.Invoke();
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

    private void OnWeaponsHiden()
    {
        _notify.Show("Вор Братишкин украл у вас оружие! Оно будет недоступно в течении 5 секунд");
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
        RegularSpeed = CurrentSpeed = PlayerData.CalculatePropertieValue(PlayerData.Properties.MoveSpeed, OriginalSpeed);
    }

    private void UpdateMaxHealthValue(bool setHealthToMax)
    {
        int maxHealth = (int)PlayerData.CalculatePropertieValue(PlayerData.Properties.Health, Health.OriginalMaxHealth);
        Health.SetMaxHealth(maxHealth, setHealthToMax);
    }

    private void UpdateDistance()
    {
        float newRadius = PlayerData.CalculatePropertieValue(PlayerData.Properties.Distance, _enemyDetection.OriginalRadius);
        _enemyDetection.SetDetectionRadius(newRadius);

        newRadius = PlayerData.CalculatePropertieValue(PlayerData.Properties.Distance, _bitsDetection.OriginalRadius);
        _bitsDetection.SetDetectionRadius(newRadius);
    }
}
