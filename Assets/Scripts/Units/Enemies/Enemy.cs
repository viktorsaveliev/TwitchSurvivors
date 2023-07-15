using System;
using System.Collections;
using UnityEngine;

public abstract class Enemy : Unit
{
    public event Action<string> OnNicknameChanged;
    public event Action<Enemy> OnEnemyDead;

    protected Transform Target;
    protected int Damage;

    private Coroutine _recoverySpeed;

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
            player.Health.TakeDamage(Damage);
        }
    }

    public virtual void Init(Transform target)
    {
        base.Init();

        Target = target;
        IsCanMove = true;
    }

    public virtual void OnSpawn()
    {
        CurrentSpeed = RegularSpeed;

        DeathFX.transform.parent = transform;
        transform.localScale = Vector2.one;
    }

    protected override void DeInit()
    {
        Health.SetHealth(Health.MaxValue);
        if (_recoverySpeed != null) StopCoroutine(_recoverySpeed);
    }

    protected override void OnTakedDamage()
    {
        base.OnTakedDamage();
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
        OnEnemyDead?.Invoke(this);
    }

    private IEnumerator RecoverySpeed()
    {
        yield return new WaitForSeconds(0.5f);
        CurrentSpeed = RegularSpeed;

        _recoverySpeed = null;
    }
}
