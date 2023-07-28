using DG.Tweening;
using UnityEngine;

public class MikeUrine : PlayerBullet
{
    public override void Init(int damage)
    {
        base.Init(damage);
        LifeTime = 5f;
    }

    public override void Shoot(Vector2 startPosition, Transform target, float speed)
    {
        base.Shoot(startPosition, target, speed);

        transform.parent = null;
        transform.position = startPosition;
        transform.localScale = Vector2.zero;
        transform.DOScale(3f, 0.5f);
    }

    protected override void OnLifeTimeEnded()
    {
        base.OnLifeTimeEnded();
        transform.DOScale(0, 0.5f).OnComplete(() => gameObject.SetActive(false));
    }
}
