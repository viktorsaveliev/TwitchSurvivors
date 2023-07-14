using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner
{
    public event Action<Enemy> OnEnemySpawned;

    private readonly MonoBehaviour _monoBehaviour;
    private readonly EnemyFactory _enemyFactory;
    private readonly Transform _target;
    
    private readonly int _poolSizeForRegularEnemies = 20;
    
    private readonly float _spawnInterval = 2f;
    private readonly Collider2D _spawnArea;

    private RegularEnemy[] _regularEnemyPool;
    private Coroutine _spawnTimer;

    private bool _isSpawnerActive;

    public EnemySpawner(MonoBehaviour monoBehaviour, EnemyFactory enemyFactory, Transform target, Collider2D spawnArea)
    {
        _monoBehaviour = monoBehaviour;
        _enemyFactory = enemyFactory;
        _target = target;
        _spawnArea = spawnArea;
    }

    public void Init()
    {
        CreateEnemies();
        StartSpawnEnemies();
    }

    public void StartSpawnEnemies()
    {
        _isSpawnerActive = true;

        if(_spawnTimer != null)
        {
            _monoBehaviour.StopCoroutine(_spawnTimer);
        }

        _spawnTimer = _monoBehaviour.StartCoroutine(SpawnTimer());
    }

    public void StopSpawnEnemies()
    {
        _isSpawnerActive = false;

        if (_spawnTimer != null)
        {
            _monoBehaviour.StopCoroutine(_spawnTimer);
            _spawnTimer = null;
        }
    }

    private void CreateEnemies()
    {
        _regularEnemyPool = new RegularEnemy[_poolSizeForRegularEnemies];

        for (int i = 0; i < _poolSizeForRegularEnemies; i++)
        {
            RegularEnemy enemy = _enemyFactory.CreateRegularEnemy(Vector3.zero);

            enemy.Init(_target);
            enemy.gameObject.SetActive(false);

            _regularEnemyPool[i] = enemy;
        }
    }

    private IEnumerator SpawnTimer()
    {
        WaitForSeconds spawnInterval = new(_spawnInterval);

        while(_isSpawnerActive)
        {
            yield return spawnInterval;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        foreach(RegularEnemy enemy in _regularEnemyPool)
        {
            if(enemy.gameObject.activeSelf) continue;

            Bounds bounds = _spawnArea.bounds;

            float randomX = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
            float randomY = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);

            enemy.transform.position = new Vector2(randomX, randomY);
            enemy.gameObject.SetActive(true);
            enemy.OnSpawn();

            OnEnemySpawned?.Invoke(enemy);
            break;
        }
    }
}
