using UnityEngine;
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
    [Inject] private readonly Notify _notify;
    [Inject] private readonly TwitchIntegration _twitch;
    [Inject] private readonly BitsController _bits;
    [Inject] private readonly Timer _timer;
    [Inject] private readonly DeathFXController _deathFX;

    [SerializeField] private PlayerDataShower _dataShower;
    [SerializeField] private PlayerDataShower _winDataShower;
    [SerializeField] private PlayerBehaviour _playerBehaviour;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private WinScreen _winScreen;

    [SerializeField] private AudioSource _soundtrack;
    [SerializeField] private AudioClip _skillzorRap;
    [SerializeField] private AudioLowPassFilter _audioFilter;
    [SerializeField] private Collider2D _spawnArea;

    [SerializeField] private SpriteRenderer _spawnPreviewPrefab;

    private Shop _shop;
    private EnemySpawner _enemySpawner;
    private CameraShaker _cameraShaker;
    private CombineEnemyAndTwitch _combineEnemyAndTwitch;

    private PauseHandler _pause;
    private AudioController _audioController;

    private BossNotify _bossNotify;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Cursor.visible = false;

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

        _pause = new(this, _shopUI, _playerInput, _pauseMenu);
        _pause.Init();

        _audioController = new(_soundtrack, _audioFilter, _pause, _skillzorRap);
        _audioController.Init();

        _shop = new(_playerUnit, _itemFactory, _shopUI, _audioController);
        _shop.Init();

        _dataShower.Init(_shop, true);
        _winDataShower.Init(_winScreen, false);
        _winScreen.Init(_enemySpawner, _audioController);

        _playerBehaviour.Init();

        _combineEnemyAndTwitch = new(_twitch, _enemySpawner);
        _combineEnemyAndTwitch.Init();

        _bits.Init();

        _bossNotify = new(_notify, _enemySpawner);
        _bossNotify.Init();
    }
}
