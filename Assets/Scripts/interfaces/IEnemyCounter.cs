using UnityEngine;

public interface IEnemyCounter
{
    public Vector2 GetClosestEnemyPosition(Vector2 position);
    public Vector2 GetRandomEnemyPosition(Vector2 position);
}
