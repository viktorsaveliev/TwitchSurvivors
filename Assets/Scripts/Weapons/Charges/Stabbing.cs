using UnityEngine;

public class Stabbing : PlayerBullet
{
    public override void Init(int damage)
    {
        base.Init(damage);
        LifeTime = 0.4f;
    }

    public override void Shoot(Vector2 startPosition, Transform target, float speed)
    {
        base.Shoot(startPosition, target, speed);

        transform.localPosition = startPosition;
    }

    protected override void OnLifeTimeEnded()
    {
        base.OnLifeTimeEnded();
        gameObject.SetActive(false);
    }
}
