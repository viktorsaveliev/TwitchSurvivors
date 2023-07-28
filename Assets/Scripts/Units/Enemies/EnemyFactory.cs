using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public event Action<Enemy> OnEnemyCreated;

    public IReadOnlyDictionary<EnemyType, List<Enemy>> Enemies => _enemies;
    
    [SerializeField] private Enemy[] _enemyPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private Collider2D _fightArea;

    private readonly Dictionary<EnemyType, int> _enemyCapacity = new()
    {
        { EnemyType.OMEGALUL,   30 },
        { EnemyType.Clueless,   5 },
        { EnemyType.EZ,         4 },
        { EnemyType.Smile_D,    6 },
        { EnemyType.catDespair, 2 },
        { EnemyType.xdd,        5 },
        { EnemyType.EBLAN,      15 },
        { EnemyType.Starege,    2 },
        { EnemyType.KEKW,       2 },
        { EnemyType.Chel,       4 },
        { EnemyType.Basedge,    2 },
        { EnemyType.Jokerge,    2 },
        { EnemyType.rjaka,      2 },
        { EnemyType.BibleThump, 8 },
        { EnemyType.Smile2,     1 },
        { EnemyType.Bratishkin, 1 }
    };

    private readonly Dictionary<EnemyType, List<Enemy>> _enemies = new();

    public enum EnemyType
    {
        OMEGALUL,
        Clueless,
        EZ,
        Smile_D,
        catDespair,
        xdd,
        EBLAN,
        Starege,
        KEKW,
        Chel,
        Basedge,
        Jokerge,
        rjaka,
        BibleThump,
        Smile2,

        Bratishkin
    }

    public void CreateEnemies()
    {
        foreach (var pair in _enemyCapacity)
        {
            for (int i = 0; i < pair.Value; i++)
            {
                Enemy enemy = Instantiate(_enemyPrefab[(int) pair.Key], Vector2.zero, Quaternion.identity, _container);

                enemy.Init();
                enemy.gameObject.SetActive(false);

                if (!_enemies.ContainsKey(pair.Key))
                {
                    _enemies[pair.Key] = new List<Enemy>();
                }
                _enemies[pair.Key].Add(enemy);

                if (enemy is IBoss boss)
                {
                    boss.SetFightArea(_fightArea);
                }

                OnEnemyCreated?.Invoke(enemy);
            }
        }
    }

    public Enemy CreateEnemy(EnemyType type, Vector3 position)
    {
        Enemy enemy = Instantiate(_enemyPrefab[(int) type], position, Quaternion.identity, _container);
        _enemies[type].Add(enemy);

        OnEnemyCreated?.Invoke(enemy);
        return enemy;
    }
}
