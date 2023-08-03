using System.Collections;
using UnityEngine;

public class EnemyBlaster : Weapon
{
    [SerializeField] private bool _shootingWithDelay;
    [SerializeField] private int _chargesCount = 10;

    private readonly float _bulletSpeed = 20f;

    public override void Improve()
    {

    }

    public override void Init()
    {
        Name = "Blaster";

        SetCooldown(5f);
        SetDamage(15);

        ChargesCount = CurrentChargesCount = _chargesCount;
        CreateCharges(Damage, true);
    }

    public void Shoot()
    {
        if (_shootingWithDelay)
        {
            StartCoroutine(ShootWithDelay());
        }
        else
        {
            ShootWithoutDelay();
        }
    }

    private void ShootWithoutDelay()
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

    private IEnumerator ShootWithDelay()
    {
        WaitForSeconds delay = new(0.05f);

        for (int i = 0; i < ChargesCount; i++)
        {
            float angle = i * 360f / ChargesCount;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Vector3 direction = rotation * Vector3.up;

            Projectile projectile = (Projectile)ChargesList[i];
            projectile.SetDirection(direction);
            projectile.Shoot(transform.position, null, _bulletSpeed);
            yield return delay;
        }
    }
}
