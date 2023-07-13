using UnityEngine;

public interface ICharge
{
    public virtual void Init()
    {

    }

    public void Shoot(Vector2 startPosition, Vector2 direction);

    protected virtual void OnLifeTimeEnded()
    {

    }
}
