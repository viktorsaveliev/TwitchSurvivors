

using System;
using UnityEngine;

public sealed class PlayerUnit : Unit
{
    [SerializeField] private BitsDetection _bitsDetection;
    public BitsDetection BitsDetection => _bitsDetection;

    public UnitWeapons Weapons { get; private set; }
    public Experience Experience { get; private set; }

    public override void Init()
    {
        base.Init();

        CurrentSpeed = 7;
        Health.SetMaxHealth(100);

        Weapons = new(this);

        Experience = new();
        Experience.Init();
    }

    protected override void DeInit()
    {
        Health.SetHealth(Health.MaxValue);
    }
}
