using UnityEngine;
using Zenject;

public class DeathFXController : MonoBehaviour
{
    [Inject] private readonly EnemyFactory _enemyFactory;

    [SerializeField] private ParticleSystem _prefabDeathFX;
    [SerializeField] private Transform _container;

    private const int _capacity = 20;
    private readonly ParticleSystem[] _deathFXs = new ParticleSystem[_capacity];

    public void Init()
    {
        CreateDeathFXs();

        _enemyFactory.OnEnemyCreated += FollowToDeath;
    }

    private void CreateDeathFXs()
    {
        for (int i = 0; i < _capacity; i++)
        {
            _deathFXs[i] = Instantiate(_prefabDeathFX, _container);
            _deathFXs[i].gameObject.SetActive(false);
        }
    }

    private void FollowToDeath(Enemy enemy)
    {
        enemy.OnDead += EnableFX;
    }

    private void EnableFX(Enemy enemy)
    {
        ParticleSystem fx = FindFreeFX();
        if (fx == null) return;

        fx.gameObject.SetActive(true);
        fx.transform.position = enemy.transform.position;
        fx.Play();
    }

    private ParticleSystem FindFreeFX()
    {
        ParticleSystem freeFX = null;

        foreach (ParticleSystem fx in _deathFXs)
        {
            if (fx.gameObject.activeSelf) continue;
            freeFX = fx;
            break;
        }

        return freeFX;
    }
}
