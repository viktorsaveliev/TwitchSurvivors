using UnityEngine;
using Zenject;

public sealed class Bootstrap : MonoBehaviour
{
    [Inject] private readonly PlayerUnit _playerUnit;
    [Inject] private readonly EnemyFactory _enemyFactory;

    [SerializeField] private Collider2D _spawnArea;
    [SerializeField] private PlayerBehaviour _playerBehaviour;

    private EnemySpawner _enemySpawner;
    private CameraShaker _cameraShaker;

    private void Awake()
    {
        _playerUnit.Init();

        _enemySpawner = new(this, _enemyFactory, _playerUnit.transform, _spawnArea);
        _enemySpawner.Init();

        _cameraShaker = new(_playerUnit);
        _cameraShaker.Init();

        _playerBehaviour.Init();
    }
}
