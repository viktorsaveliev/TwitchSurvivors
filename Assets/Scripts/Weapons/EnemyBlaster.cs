using UnityEngine;

public class EnemyBlaster : Weapon
{
    private readonly float _bulletSpeed = 20f;

    public override void Improve()
    {

    }

    public override void Init()
    {
        Name = "Bratishkin blaster";

        SetCooldown(5f);
        SetDamage(15);

        ChargesCount = CurrentChargesCount = 10;
        CreateCharges(Damage, true);
    }

    public void Shoot()
    {
        for (int i = 0; i < ChargesCount; i++)
        {
            float angle = i * 360f / ChargesCount;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Vector3 direction = rotation * Vector3.up;

            Projectile projectile = (Projectile)ChargesList[i];
            projectile.SetDirection(direction);
            projectile.Shoot(transform.position, null, _bulletSpeed);
        }
    }
}
