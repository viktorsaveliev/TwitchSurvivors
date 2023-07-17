using UnityEngine;

public sealed class PlayerUnit : Unit
{
    [SerializeField] private EnemiesDetection _enemyDetection;

    public UnitWeapons Weapons { get; private set; }
    public Experience Experience { get; private set; }

    public override void Init()
    {
        base.Init();

        RegularSpeed = CurrentSpeed = 7;
        Weapons = new(this);

        Experience = new();
        Experience.Init();

        _enemyDetection.Init();

        Health.SetMaxHealth(100, true);

        UpdateProperties();
    }

    public void UpdateProperties()
    {
        UpdateMoveSpeed();
        UpdateMaxHealthValue(false);
        UpdateAttackDistance();
    }

    protected override void DeInit()
    {
        Health.SetHealth(Health.MaxValue);
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

    private void UpdateAttackDistance()
    {
        float newRadius = PlayerData.CalculatePropertieValue(PlayerData.Properties.AttackDistance, _enemyDetection.CurrentRadius);
        _enemyDetection.UpdateRadius(newRadius);
    }
}
