using System;
using UnityEngine;

public abstract class PlayerBullet : Bullet
{
    public event Action OnHitEnemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            int damage = (int)PlayerData.CalculateValueWithPropertie(PlayerData.Properties.Damage, Damage);
            enemy.Health.TakeDamage(damage);

            OnHitEnemy?.Invoke();
        }
    }
}
