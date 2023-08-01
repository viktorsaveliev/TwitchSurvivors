using UnityEngine;

public class Jokerge : Enemy
{
    [SerializeField] private EnemyPistol _weaponPrefab;

    private EnemyPistol _weapon;

    public override void Init()
    {
        base.Init();

        CreateWeapon();
    }

    private void CreateWeapon()
    {
        if (_weaponPrefab == null) return;

        _weapon = Instantiate(_weaponPrefab, transform);
        _weapon.Init();
    }

    private void Attack()
    {
        if (!gameObject.activeSelf) return;

        _weapon.Shoot(Target);
        Invoke(nameof(Attack), _weapon.Cooldown);
    }

    public override void OnSpawn()
    {
        base.OnSpawn();

        Invoke(nameof(Attack), _weapon.Cooldown);
    }
}
