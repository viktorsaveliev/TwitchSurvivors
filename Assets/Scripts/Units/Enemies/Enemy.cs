using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public abstract class Enemy : Unit
{
    [SerializeField] protected EnemyDataConfig _config;
    [SerializeField] private string _nickname;
    [SerializeField] private Transform _skinTransform;

    public bool IsOnSpawnProccess;

    public event Action<string> OnNicknameChanged;
    public event Action<Enemy> OnDead;

    protected Transform Target;
    protected int Damage;
    protected bool OnLeft;

    private Coroutine _recoverySpeed;
    private Vector2 _regularScale;
    private Tween _tweenAnim;

    public string Nickname
    {
        get
        {
            return _nickname;
        }

        set
        {
            if (_nickname != value)
            {
                _nickname = value;
                OnNicknameChanged?.Invoke(value);
            }
        }
    }

    public bool IsCanMove { get; protected set; }

    protected virtual void FixedUpdate()
    {
        if (IsCanMove && Target != null)
        {
            Vector2 direction = Target.position - transform.position;
            Move(direction);

            if (OnLeft && direction.x < 0)
            {
                Flip(false);
            }
            else if (!OnLeft && direction.x > 0)
            {
                Flip(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerUnit player))
        {
            if (!player.IsDodged())
            {
                player.LastHittedEnemyNickname = Nickname;
                
                int damage = (int)PlayerData.CalculatePropertieValue(PlayerData.Properties.Armor, Damage, false);
                player.Health.TakeDamage(damage);
            }
        }
    }

    public override void Init()
    {
        base.Init();

        _nickname = string.Empty;

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

    private void Flip(bool onLeft)
    {
        OnLeft = onLeft;
        _tweenAnim?.Complete();
        _tweenAnim = _skinTransform
            .DOScale(new Vector2(-_skinTransform.localScale.x, _skinTransform.localScale.y), 0.2f)
            .OnComplete(() => _tweenAnim = null);
    }
}
