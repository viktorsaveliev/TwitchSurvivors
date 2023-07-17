using UnityEngine;

public class UnitAnimation
{
    private readonly Animator _animator;
    private readonly Unit _unit;

    public UnitAnimation(Animator animator, Unit unit)
    {
        _animator = animator;
        _unit = unit;
    }

    public void Init()
    {
        _unit.Health.OnTakedDamage += OnTakeDamage;
    }

    private void OnTakeDamage(int damage)
    {
        _animator.SetTrigger("TakeDamage");
    }
}
