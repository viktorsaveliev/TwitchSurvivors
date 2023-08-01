using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class LevelEntryPoint : MonoBehaviour
{
    [Header("Player Data")]
    [Inject] private readonly PlayerUnit _playerUnit;
    [Inject] private readonly ShopUI _shopUI;
    [Inject] private readonly IInputControl _playerInput;

    [Header("Factories")]
    [Inject] private readonly ItemFactory _itemFactory;
    [Inject] private readonly EnemyFactory _enemyFactory;

    [Header("Others")]
    [Inject] private readonly TwitchIntegration _twitch;
    [Inject] private readonly BitsController _bits;
    [Inject] private readonly Timer _timer;
    [Inject] private readonly DeathFXController _deathFX;

    [SerializeField] private PlayerDataShower _dataShower;
    [SerializeField] private PlayerBehaviour _playerBehaviour;
    [SerializeField] private PauseMenu _pauseMenu;

    [SerializeField] private AudioSource _soundtrack;
    [SerializeField] private Collider2D _spawnArea;
    [SerializeField] private AudioLowPassFilter _audioFilter;

    [SerializeField] private SpriteRenderer _spawnPreviewPrefab;

    private Shop _shop;
    private EnemySpawner _enemySpawner;
    private CameraShaker _cameraShaker;
    private CombineEnemyAndTwitch _combineEnemyAndTwitch;

    private PauseHandler _pause;
    private AudioController _audioController;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Money.Set(70);

        _playerUnit.Init();
        _shopUI.Init();

        _deathFX.Init();

        _itemFactory.Init();

        _timer.Init(_playerUnit);

        _enemySpawner = new(this, _enemyFactory, _playerUnit.transform, _spawnArea, _spawnPreviewPrefab);
        _enemySpawner.Init();

        _cameraShaker = new(_playerUnit);
        _cameraShaker.Init();

        _shop = new(_playerUnit, _itemFactory, _shopUI);
        _shop.Init();

        _dataShower.Init(_shop);

        _playerBehaviour.Init();

        _combineEnemyAndTwitch = new(_twitch, _enemySpawner);
        _combineEnemyAndTwitch.Init();

        _bits.Init();

        _pause = new(this, _shopUI, _playerInput, _pauseMenu);
        _pause.Init();

        _audioController = new(_soundtrack, _audioFilter, _pause);
        _audioController.Init();
    }
}
