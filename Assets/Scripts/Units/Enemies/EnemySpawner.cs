using System;
using System.Collections;
using Unity.Mathematics;
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
        float timerLength = 0;

        while(_isSpawnerActive)
        {
            yield return spawnInterval;
            timerLength += _spawnInterval;

            if (timerLength > 30)
            {
                SpawnEnemiesAroundPlayer();
                timerLength = 0;
            }
            else
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        Enemy enemy = GetRandomRegularEnemy();

        if (enemy == null) return;

        Bounds bounds = _spawnArea.bounds;

        float randomX = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float randomY = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);

        enemy.transform.position = new Vector2(randomX, randomY);
        enemy.gameObject.SetActive(true);
        enemy.OnSpawn();

        OnEnemySpawned?.Invoke(enemy);
    }

    private Enemy GetRandomRegularEnemy()
    {
        Enemy randomEnemy = null;

        foreach (RegularEnemy enemy in _regularEnemyPool)
        {
            if (enemy.gameObject.activeSelf || enemy.CurrentSpawnDelay > Time.time) continue;
            randomEnemy = enemy;
        }

        return randomEnemy;
    }

    private void SpawnEnemiesAroundPlayer()
    {
        float spawnRadius = 40f;
        float minDistanceFromPlayer = 30f;

        int enemiesCount = 10;
        int currentEnemiesSpawned = 0;

        for (int i = 0; i < _regularEnemyPool.Length; i++)
        {
            if(currentEnemiesSpawned >= enemiesCount)
            {
                break;
            }

            Enemy enemy = GetRandomRegularEnemy();

            if (enemy != null)
            {
                Vector2 randomOffset = UnityEngine.Random.insideUnitCircle.normalized * (spawnRadius - minDistanceFromPlayer);
                Vector2 spawnPosition = (Vector2)_target.transform.position + randomOffset;

                bool isSpawnPointInArea = _spawnArea.OverlapPoint(spawnPosition);
                if (!isSpawnPointInArea) continue;

                enemy.transform.position = spawnPosition;
                enemy.gameObject.SetActive(true);
                enemy.OnSpawn();

                OnEnemySpawned?.Invoke(enemy);

                currentEnemiesSpawned++;
            }
        }
    }
}
