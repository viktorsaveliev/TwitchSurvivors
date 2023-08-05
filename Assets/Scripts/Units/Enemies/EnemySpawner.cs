using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner
{
    public event Action<Enemy> OnEnemySpawned;

    private readonly MonoBehaviour _monoBehaviour;
    private readonly EnemyFactory _enemyFactory;
    private readonly Transform _target;
    
    private readonly float _minDistanceToTarget = 3f;
    private readonly float _spawnInterval = 2f;
    private readonly Collider2D _spawnArea;

    private Coroutine _spawnTimer;

    private bool _isSpawnerActive;

    private readonly SpriteRenderer _previewPrefab;
    private readonly SpriteRenderer[] _spawnPreview = new SpriteRenderer[_previewCapacity];
    private const int _previewCapacity = 50;

    private readonly WaitForSeconds _delayForSpawn = new(0.9f);

    private int _currentWave = 0;

    public EnemySpawner(MonoBehaviour monoBehaviour, 
                        EnemyFactory enemyFactory, 
                        Transform target, 
                        Collider2D spawnArea, 
                        SpriteRenderer previewPrefab)
    {
        _monoBehaviour = monoBehaviour;
        _enemyFactory = enemyFactory;
        _target = target;
        _spawnArea = spawnArea;
        _previewPrefab = previewPrefab;
    }

    public void Init()
    {
        _enemyFactory.CreateEnemies();
        CreatePreviewObjects();
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

    private void CreatePreviewObjects()
    {
        for (int i = 0; i < _previewCapacity; i++)
        {
            _spawnPreview[i] = UnityEngine.Object.Instantiate(_previewPrefab, _monoBehaviour.transform);
            _spawnPreview[i].enabled = false;
        }
    }

    private SpriteRenderer FindFreePreview()
    {
        SpriteRenderer preview = null;

        foreach (SpriteRenderer sprite in _spawnPreview)
        {
            if (sprite.enabled) continue;
            preview = sprite;
            break;
        }

        return preview;
    }

    private IEnumerator SpawnTimer()
    {
        WaitForSeconds spawnInterval = new(_spawnInterval);
        float timerLength = 0;

        while(_isSpawnerActive)
        {
            yield return spawnInterval;

            timerLength += _spawnInterval;

            if (_currentWave == 0)
            {
                FirstEnemyWave(timerLength);
            }
            else
            {
                SecondEnemyWave(timerLength);
            }
        }
    }

    private void FirstEnemyWave(float time)
    {
        switch (time)
        {
            case 4:
                SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.EZ);
                //StopSpawnEnemies();
                break;

            case 20:
            case 30:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.EBLAN, 5);
                break;

            case 40:
            case 50:
                SpawnGroupInRandomPoint(EnemyFactory.EnemyType.xdd, 10);
                break;

            case 60:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Clueless, 10);
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.EBLAN, 15);
                break;

            case 66:
            case 68:
                SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.Starege);
                break;

            case 70:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Starege, 5);
                SpawnGroupInRandomPoint(EnemyFactory.EnemyType.xdd, 5);
                break;

            case 72:
            case 74:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.EBLAN, 10);
                break;

            case 76:
            case 80:
            case 84:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Jokerge, 3);
                break;

            case 86:
            case 90:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Jokerge, 10);
                break;

            case 100:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.EBLAN, 10);
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Jokerge, 5);
                //SpawnGroupInRandomPoint(EnemyFactory.EnemyType.xdd, 5);
                break;

            case 98:
            case 106:
            case 110:
            case 114:
            case 116:
            case 120:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Starege, 5);
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.EBLAN, 2);
                break;

            case 130:
            case 140:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Starege, 15);
                break;

            case 160:
            case 180:
                SpawnEnemiesAroundTarget(EnemyFactory.EnemyType.EBLAN);
                break;

            case 182:
                SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.BigSmile);
                break;

            case 186:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.EBLAN, 20);
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Starege, 10);
                break;

            case 194:
                SpawnGroupInRandomPoint(EnemyFactory.EnemyType.xdd, 5);
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Clueless, 10);
                break;

            case 200:
                SpawnGroupInRandomPoint(EnemyFactory.EnemyType.xdd, 5);
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Starege, 5);
                break;

            case 230:
            case 240:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Jokerge, 10);
                break;

            case 260:
            case 264:
            case 268:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.catDespair, 20);
                break;

            case 280:
                SpawnGroupInRandomPoint(EnemyFactory.EnemyType.xdd, 5);
                SpawnGroupInRandomPoint(EnemyFactory.EnemyType.xdd, 5);
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Starege, 5);
                break;

            case 290:
                SpawnGroupInRandomPoint(EnemyFactory.EnemyType.xdd, 5);
                SpawnGroupInRandomPoint(EnemyFactory.EnemyType.xdd, 5);
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Jokerge, 5);
                break;

            case 300:
                SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.Bratishkin);
                StopSpawnEnemies();
                break;

            default:
                SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.EBLAN);
                break;
        }
    }

    private void SecondEnemyWave(float time)
    {
        switch (time)
        {
            case 20:
            case 30:
            case 40:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Chel, 10);
                break;

            case 70:
            case 80:
            case 90:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Chel, 5);
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.BibleThump, 10);
                break;

            case 98:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Starege, 20);
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Jokerge, 10);
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.EBLAN, 15);
                break;

            case 106:
            case 110:
            case 114:
            case 116:
            case 120:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.KEKW, 5);
                break;

            case 130:
            case 140:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.EBLAN, 10);
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Jokerge, 15);
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Starege, 10);
                break;

            case 160:
            case 180:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Starege, 10);
                SpawnEnemiesAroundTarget(EnemyFactory.EnemyType.Basedge);
                break;

            case 182:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Starege, 20);
                SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.BigSmile);
                break;

            case 186:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Chel, 30);
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Starege, 15);
                break;

            case 194:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.EBLAN, 10);
                SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.BigSmile);
                SpawnGroupInRandomPoint(EnemyFactory.EnemyType.xdd, 5);
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Clueless, 10);
                break;

            case 200:
                SpawnGroupInRandomPoint(EnemyFactory.EnemyType.xdd, 5);
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Starege, 5);
                break;

            case 210:
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.KEKW, 10);
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.EBLAN, 10);
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Jokerge, 15);
                SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType.Starege, 5);
                break;

            case 250:
                SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.WhiteRa);
                StopSpawnEnemies();
                break;

            default:
                SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.Chel);
                break;
        }
    }

    private void SpawnEnemyInRandomPoint(EnemyFactory.EnemyType type)
    {
        Enemy enemy = GetFreeEnemy(type);
        if (enemy == null) return;

        Vector2 enemyPosition = GetRandomPosition();
        if (Vector2.Distance(enemyPosition, _target.position) < _minDistanceToTarget) return;

        _monoBehaviour.StartCoroutine(SpawnEnemy(enemy, enemyPosition));

        if (enemy is IBoss)
        {
            enemy.OnDead += StartSecondWave;
        }
    }

    private void StartSecondWave(Enemy enemy)
    {
        enemy.OnDead -= StartSecondWave;
        _currentWave = 1;

        StartSpawnEnemies();
    }

    private void SpawnEnemiesInRandomPoints(EnemyFactory.EnemyType type, int capacity)
    {
        for (int i = 0; i < capacity; i++)
        {
            SpawnEnemyInRandomPoint(type);
        }
    }

    private IEnumerator SpawnEnemy(Enemy enemy, Vector2 position)
    {
        enemy.IsOnSpawnProccess = true;

        SpriteRenderer preview = FindFreePreview();
        if (preview != null)
        {
            preview.transform.position = position;
            preview.transform.localScale = Vector2.zero;
            preview.enabled = true;

            preview.transform.DOScale(2f, 0.5f).SetEase(Ease.OutBack);
        }

        yield return _delayForSpawn;

        enemy.transform.position = position;
        enemy.gameObject.SetActive(true);
        enemy.SetTarget(_target);
        enemy.OnSpawn();

        OnEnemySpawned?.Invoke(enemy);

        preview.enabled = false;
    }

    private Vector2 GetRandomPosition()
    {
        Bounds bounds = _spawnArea.bounds;

        float randomX = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float randomY = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);

        return new(randomX, randomY);
    }

    private Enemy GetFreeEnemy(EnemyFactory.EnemyType type)
    {
        Enemy randomEnemy = null;

        foreach (Enemy enemy in _enemyFactory.Enemies[type])
        {
            if (enemy.gameObject.activeSelf || enemy.CurrentSpawnDelay > Time.time || enemy.IsOnSpawnProccess) continue;
            randomEnemy = enemy;
        }

        return randomEnemy;
    }

    private void SpawnGroupInRandomPoint(EnemyFactory.EnemyType type, int capacity)
    {
        Vector2 enemyPosition = GetRandomPosition();
        if (Vector2.Distance(enemyPosition, _target.position) < _minDistanceToTarget) return;

        for (int i = 0; i < capacity; i++)
        {
            Enemy enemy = GetFreeEnemy(type);

            if (enemy == null)
            {
                break;
            }

            Vector2 randomPosition = new(
                enemyPosition.x + UnityEngine.Random.Range(-2f, 2f), 
                enemyPosition.y + UnityEngine.Random.Range(-2f, 2f)
                );

            _monoBehaviour.StartCoroutine(SpawnEnemy(enemy, randomPosition));
        }
    }

    private void SpawnEnemiesAroundTarget(EnemyFactory.EnemyType type)
    {
        float spawnRadius = 40f;
        float minDistanceFromPlayer = 30f;

        int enemiesCount = 10;
        int currentEnemiesSpawned = 0;

        for (int i = 0; i < _enemyFactory.Enemies[type].Count; i++)
        {
            if(currentEnemiesSpawned >= enemiesCount)
            {
                break;
            }

            Enemy enemy = GetFreeEnemy(type);

            if (enemy != null)
            {
                Vector2 randomOffset = UnityEngine.Random.insideUnitCircle.normalized * (spawnRadius - minDistanceFromPlayer);
                Vector2 spawnPosition = (Vector2)_target.transform.position + randomOffset;

                bool isSpawnPointInArea = _spawnArea.OverlapPoint(spawnPosition);
                if (!isSpawnPointInArea) continue;

                _monoBehaviour.StartCoroutine(SpawnEnemy(enemy, spawnPosition));

                currentEnemiesSpawned++;
            }
        }
    }
}