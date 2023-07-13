using UnityEngine;

public abstract class Enemy : Unit
{
    protected Transform Target;
    protected int Damage;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerUnit player))
        {
            player.Health.TakeDamage(Damage);
        }
    }
}
