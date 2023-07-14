using DG.Tweening;
using System;
using UnityEngine;

public abstract class Enemy : Unit
{
    public event Action<string> OnNicknameChanged;
    protected Transform Target;
    protected int Damage;

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

    public virtual void Init(Transform target)
    {
        base.Init();

        Target = target;
        IsCanMove = true;
    }

    protected override void DeInit()
    {
        Health.SetHealth(Health.MaxValue);
    }

    public virtual void OnSpawn()
    {
        transform.localScale = Vector2.zero;
        transform.DOScale(1, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerUnit player))
        {
            player.Health.TakeDamage(Damage);
        }
    }
}
