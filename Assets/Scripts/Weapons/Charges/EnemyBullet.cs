using System;
using UnityEngine;

public abstract class EnemyBullet : Bullet
{
    public event Action OnHitPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerUnit player))
        {
            player.Health.TakeDamage(Damage);
            OnHitPlayer?.Invoke();
        }
    }
}
