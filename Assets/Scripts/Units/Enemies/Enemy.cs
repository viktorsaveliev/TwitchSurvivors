using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public abstract class Enemy : Unit
{
    [SerializeField] protected EnemyDataConfig _config;

    public bool IsOnSpawnProccess;

    public event Action<string> OnNicknameChanged;
    public event Action<Enemy> OnDead;

    protected Transform Target;
    protected int Damage;

    private Coroutine _recoverySpeed;
    private Vector2 _regularScale;

    public string Nickname 
    {
        get
        {
            return Nickname;
        }

        set
        {
            OnNicknameChanged?.Invoke(value);
        }
    }

    public bool IsCanMove { get; protected set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerUnit player))
        {
            if (!player.IsDodged())
            {
                int damage = (int)PlayerData.CalculateValueWithPropertie(PlayerData.Properties.Armor, Damage, false);
                player.Health.TakeDamage(damage);
            }
        }
    }

    public override void Init()
    {
        base.Init();
        IsCanMove = true;
        _regularScale = transform.localScale;

        RegularSpeed = CurrentSpeed = _config.Speed;
        Damage = _config.Damage;

        Health.SetMaxHealth(_config.Health, true);

        if (_config.Shield > 0)
        {
            Health.SetImmunity(_config.Shield);
        }
    }

    public virtual void OnSpawn()
    {
        IsOnSpawnProccess = false;

        CurrentSpeed = RegularSpeed;
        transform.localScale = _regularScale;
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    protected override void DeInit()
    {
        Health.SetHealth(Health.MaxValue);
        if (_recoverySpeed != null) StopCoroutine(_recoverySpeed);
    }

    protected override void OnTakedDamage(int damage)
    {
        base.OnTakedDamage(damage);
        CurrentSpeed = 1;

        if (_recoverySpeed != null) StopCoroutine(_recoverySpeed);

        if (gameObject.activeSelf)
        {
            _recoverySpeed = StartCoroutine(RecoverySpeed());
        }
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        OnDead?.Invoke(this);
    }

    private IEnumerator RecoverySpeed()
    {
        yield return new WaitForSeconds(0.5f);
        CurrentSpeed = RegularSpeed;

        _recoverySpeed = null;
    }
}
