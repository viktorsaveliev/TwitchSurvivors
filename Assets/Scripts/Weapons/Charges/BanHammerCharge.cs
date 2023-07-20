using DG.Tweening;
using UnityEngine;

public class BanHammerCharge : Bullet
{
    public override void Init(int damage)
    {
        base.Init(damage);
        LifeTime = 0.5f;
    }

    public override void Shoot(Vector2 startPosition, Vector2 direction, float speed)
    {
        base.Shoot(startPosition, direction, speed);

        transform.position = startPosition;
        transform.localScale = Vector2.zero;
        transform.DOScale(2f, 0.2f).OnComplete(() => gameObject.SetActive(false));
    }
}
