using UnityEngine;
using Zenject;

public sealed class Bootstrap : MonoBehaviour
{
    [Header("Player Data")]
    [Inject] private readonly PlayerUnit _playerUnit;
    [Inject] private readonly PlayerInterface _playerUI;

    [Header("Factories")]
    [Inject] private readonly ItemFactory _itemFactory;
    [Inject] private readonly EnemyFactory _enemyFactory;

    [Header("Others")]
    [Inject] private readonly TwitchIntegration _twitch;
    [Inject] private readonly BitsController _bits;
    [Inject] private readonly Timer _timer;
    [Inject] private readonly DeathFXController _deathFX;

    [SerializeField] private Collider2D _spawnArea;
    [SerializeField] private PlayerBehaviour _playerBehaviour;
    [SerializeField] private AudioLowPassFilter _audioFilter;

    private Shop _shop;
    private EnemySpawner _enemySpawner;
    private CameraShaker _cameraShaker;
    private CombineEnemyAndTwitch _combineEnemyAndTwitch;

    private PauseHandler _pause;
    private AudioController _audioController;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Money.Set(0);

        _playerUnit.Init();
        _playerUI.Init();

        _deathFX.Init();

        _itemFactory.Init();

        _timer.Init(_playerUnit);

        _enemySpawner = new(this, _enemyFactory, _playerUnit.transform, _spawnArea);
        _enemySpawner.Init();

        _cameraShaker = new(_playerUnit);
        _cameraShaker.Init();

        _playerBehaviour.Init();

        _combineEnemyAndTwitch = new(_twitch, _enemySpawner);
        _combineEnemyAndTwitch.Init();

        _bits.Init();

        _shop = new(_playerUnit, _itemFactory, _playerUI);
        _shop.Init();

        _pause = new(this, _playerUI);
        _pause.Init();

        _audioController = new(_audioFilter, _pause);
        _audioController.Init();
    }
}
