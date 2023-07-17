using DG.Tweening;
using UnityEngine;

public class Cogger : Bullet
{
    private readonly float _rotationSpeed = 100f;

    public override void Init(int damage)
    {
        base.Init(damage);
        LifeTime = 5f;
    }

    private void Update()
    {
        transform.Rotate(_rotationSpeed * Time.deltaTime * Vector3.forward);
    }

    public void SetPosition(Vector2 position) => transform.localPosition = position;

    public override void Shoot(Vector2 startPosition, Vector2 direction)
    {
        base.Shoot(startPosition, direction);

        transform.localScale = Vector2.zero;
        transform.DOScale(1f, 0.5f);
    }

    protected override void OnHitEnemy()
    {
        
    }

    protected override void OnLifeTimeEnded()
    {
        base.OnLifeTimeEnded();
        transform.DOScale(0, 0.5f).OnComplete(() => gameObject.SetActive(false));
    }
}
