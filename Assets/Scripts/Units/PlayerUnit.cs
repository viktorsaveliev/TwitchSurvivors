

public sealed class PlayerUnit : Unit
{
    public UnitWeapons Weapons { get; private set; }

    public override void Init()
    {
        base.Init();

        Speed = 7;
        Health.SetMaxHealth(100);
        Weapons = new(this);
    }

    protected override void DeInit()
    {
        Health.SetHealth(Health.MaxValue);
    }
}
