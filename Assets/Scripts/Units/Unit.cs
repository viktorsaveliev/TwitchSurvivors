using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected ParticleSystem DeathFX;

    protected Animator Animator;
    
    private readonly float _spawnDelay = 2f;
    
    private Rigidbody2D _rigidbody;
    private UnitAnimation Animation;

    public Health Health { get; private set; }
    public float CurrentSpeed { get; protected set; }
    public float CurrentSpawnDelay { get; protected set; }
    public float RegularSpeed { get; protected set; }

    public virtual void Init()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

        Health ??= new();
        Health.OnHealthOver += OnDeath;
        Health.OnTakedDamage += OnTakedDamage;

        Animation ??= new(Animator, this);
        Animation.Init();
    }

    protected abstract void DeInit();

    public void Move(Vector2 direction)
    {
        direction.Normalize();
        _rigidbody.velocity = CurrentSpeed * direction;
    }

    protected virtual void OnTakedDamage()
    {
        
    }

    protected virtual void OnDeath()
    {
        DeInit();

        CurrentSpawnDelay = Time.time + _spawnDelay;

        DeathFX.transform.parent = null;
        DeathFX.transform.position = transform.position;
        DeathFX.Play();

        gameObject.SetActive(false);
    }
}
