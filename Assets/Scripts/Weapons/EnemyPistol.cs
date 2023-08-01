using UnityEngine;

public class EnemyPistol : Weapon
{
    private readonly float _bulletSpeed = 9f;

    public override void Improve()
    {
        
    }

    public override void Init()
    {
        Name = "Enemy pistol";

        SetCooldown(2.5f);
        SetDamage(15);

        CreateCharge(Damage, true);
    }

    public void Shoot(Transform target)
    {
        ChargesList[0].Shoot(transform.position, target, _bulletSpeed);
    }
}
