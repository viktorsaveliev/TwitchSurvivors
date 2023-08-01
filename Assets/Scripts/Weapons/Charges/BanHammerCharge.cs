using DG.Tweening;
using UnityEngine;

public class BanHammerCharge : PlayerBullet
{
    public override void Init(int damage)
    {
        base.Init(damage);
        LifeTime = 0.5f;
    }

    public override void Shoot(Vector2 startPosition, Transform target, float speed)
    {
        base.Shoot(startPosition, target, speed);

        transform.position = startPosition;
        transform.localScale = Vector2.zero;
        transform.DOScale(2f, LifeTime).OnComplete(() => gameObject.SetActive(false));
    }
}
