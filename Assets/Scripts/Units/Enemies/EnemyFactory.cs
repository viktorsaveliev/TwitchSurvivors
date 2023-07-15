using System;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private RegularEnemy _enemyPrefab;
    [SerializeField] private Transform _containerForEnemies;

    public event Action<Enemy> OnEnemyCreated;

    public RegularEnemy CreateRegularEnemy(Vector3 position)
    {
        RegularEnemy enemy = Instantiate(_enemyPrefab, position, Quaternion.identity, _containerForEnemies);
        OnEnemyCreated?.Invoke(enemy);
        return enemy;
    }
}
