using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public abstract class Unit : MonoBehaviour
{
    [SerializeField] private ParticleSystem _deathFX;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    
    private UnitAnimation Animation;

    public Health Health { get; private set; }
    public float Speed { get; protected set; }

    public virtual void Init()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        Health ??= new();
        Health.OnHealthOver += OnUnitDeath;
        Health.OnTakedDamage += OnTakedDamage;

        Animation ??= new(_animator, this);
        Animation.Init();
    }

    protected abstract void DeInit();

    public void Move(Vector2 direction)
    {
        direction.Normalize();
        _rigidbody.velocity = Speed * direction;
    }

    protected virtual void OnTakedDamage()
    {
        _deathFX.Play();
    }

    protected virtual void OnUnitDeath()
    {
        DeInit();
        gameObject.SetActive(false);
    }
}
