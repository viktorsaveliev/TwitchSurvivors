using System;

public class Health : IDamageable
{
    public event Action OnTakedDamage;
    public event Action OnHealthChanged;
    public event Action OnHealthOver;

    public int CurrentValue => _health;
    public int MaxValue => _maxHealth;

    private int _maxHealth;
    private int _health;

    public void SetMaxHealth(int value)
    {
        if (value < 10 || value > 1000) return;

        _maxHealth = value;
        SetHealth(value);
    }

    public void SetHealth(int value)
    {
        if (value < 1) return;

        _health = value > _maxHealth ? _maxHealth : value;
        OnHealthChanged?.Invoke();
    }

    public void GiveHealth(int value) => SetHealth(_health + value);

    public void MaximizeHealth(int value) => SetMaxHealth(_maxHealth + value);

    public void TakeDamage(int value)
    {
        _health -= value;
        OnTakedDamage?.Invoke();

        if (_health <= 0)
        {
            OnHealthOver?.Invoke();
        }
    }
}
