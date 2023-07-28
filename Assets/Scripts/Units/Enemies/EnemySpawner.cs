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
    private const int _previewCapacity = 30;

    private readonly WaitForSeconds _delayForSpawn = new(0.9f);

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

            switch (timerLength)
            {
                case 30:
                case 38:
                case 40:
                case 138:
                case 174:
                case 176:
                case 180:
                case 190:
                case 250:
                    SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.Smile_D);
                    break;

                case 16:
                case 26:
                case 36:
                case 46:
                case 56:

                    int capacity = UnityEngine.Random.Range(4, 8);

                    for (int i = 0; i < capacity; i++)
                    {
                        SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.OMEGALUL);
                    }

                    break;

                case 66:
                case 76:
                case 86:
                case 96:
                case 106:
                case 116:
                case 120:
                case 128:
                case 132:
                case 172:
                case 178:
                case 212:
                case 216:
                case 256:
                case 266:
                case 280:
                    capacity = UnityEngine.Random.Range(8, 15);

                    for (int i = 0; i < capacity; i++)
                    {
                        SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.OMEGALUL);
                    }

                    break;

                case 34:
                case 100:
                case 160:
                case 220:
                case 276:
                case 284:
                    SpawnEnemiesInRandomPoint(EnemyFactory.EnemyType.Chel, 4);
                    break;

                case 32:
                case 42:
                case 54:
                case 274:
                case 286:
                    SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.Clueless);
                    SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.Clueless);
                    break;

                case 20:
                case 24:
                case 50:
                case 150:
                case 154:
                case 156:
                case 158:
                case 288:
                case 290:
                    SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.EZ);
                    SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.EZ);
                    break;

                case 68:
                case 74:
                case 110:
                case 118:
                case 292:
                    SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.Jokerge);
                    SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.Jokerge);
                    break;

                case 48:
                case 70:
                case 122:
                case 146:
                case 168:
                case 186:
                case 294:
                    SpawnEnemiesInRandomPoint(EnemyFactory.EnemyType.xdd, UnityEngine.Random.Range(3, 5));
                    break;

                case 44:
                case 58:
                case 62:
                case 64:
                case 134:
                case 152:
                    SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.Starege);
                    SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.Starege);
                    break;

                case 28:
                case 78:
                case 80:
                case 82:
                case 140:
                case 148:
                    SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.Basedge);
                    SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.Basedge);
                    break;

                case 84:
                case 88:
                case 90:
                case 144:
                case 162:
                    SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.catDespair);
                    SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.catDespair);
                    break;

                case 98:
                case 104:
                case 164:
                case 166:
                    SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.KEKW);
                    SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.KEKW);
                    break;

                case 52:
                case 72:
                case 92:
                case 102:
                case 170:
                case 200:
                case 240:
                    SpawnEnemiesAroundPlayer(EnemyFactory.EnemyType.EBLAN);
                    break;

                case 60:
                case 114:
                    capacity = UnityEngine.Random.Range(3, 6);

                    for (int i = 0; i < capacity; i++)
                    {
                        SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.BibleThump);
                    }

                    break;

                case 94:
                case 112:
                case 124:
                case 126:
                    SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.BibleThump);
                    break;

                case 130:
                case 222:
                case 296:
                    SpawnEnemiesAroundPlayer(EnemyFactory.EnemyType.BibleThump);
                    break;

                case 136:
                case 210:
                    SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.Smile2);
                    break;

                case 300:
                    StopSpawnEnemies();
                    SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.Bratishkin);
                    break;

                default:
                    SpawnEnemyInRandomPoint(EnemyFactory.EnemyType.OMEGALUL);
                    break;
            }
        }
    }

    private void SpawnEnemyInRandomPoint(EnemyFactory.EnemyType type)
    {
        Enemy enemy = GetFreeEnemy(type);
        if (enemy == null) return;

        Vector2 enemyPosition = GetRandomPosition();
        if (Vector2.Distance(enemyPosition, _target.position) < _minDistanceToTarget) return;

        _monoBehaviour.StartCoroutine(SpawnEnemy(enemy, enemyPosition));
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

            preview.transform.DOScale(2f, 0.5f).SetEase(Ease.OutBounce);
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

    private void SpawnEnemiesInRandomPoint(EnemyFactory.EnemyType type, int capacity)
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

    private void SpawnEnemiesAroundPlayer(EnemyFactory.EnemyType type)
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
