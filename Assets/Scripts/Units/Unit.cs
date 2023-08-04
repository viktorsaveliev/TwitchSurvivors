using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
[RequireComponent(typeof(AudioSource), typeof(AudioVolume))]
public abstract class Unit : MonoBehaviour
{
    protected Animator Animator;
    protected Rigidbody2D Rigidbody;
    protected AudioSource SoundTakeDamage;
    
    private readonly float _spawnDelay = 2f;
    private UnitAnimation Animation;

    public Health Health { get; private set; }
    public float CurrentSpeed { get; protected set; }
    public float CurrentSpawnDelay { get; protected set; }
    public float RegularSpeed { get; protected set; }
    public float OriginalSpeed { get; protected set; }
    public float DamageImmunity { get; set; }

    public virtual void Init()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        SoundTakeDamage = GetComponent<AudioSource>();

        Health ??= new();
        Health.OnHealthOver += OnDeath;
        Health.OnTakedDamage += OnTakedDamage;

        Animation ??= new(Animator, this);
        Animation.Init();
    }

    protected abstract void DeInit();

    public virtual void Move(Vector2 direction)
    {
        direction.Normalize();
        Rigidbody.velocity = CurrentSpeed * direction;
    }

    protected virtual void OnTakedDamage(int damage)
    {
        if (Health.CurrentValue > 0)
        {
            if(SoundTakeDamage.clip != null) 
                SoundTakeDamage.Play();

            Animator.SetTrigger("TakeDamage");
        }
    }

    protected virtual void OnDeath()
    {
        DeInit();

        CurrentSpawnDelay = Time.time + _spawnDelay;
        gameObject.SetActive(false);
    }
}
