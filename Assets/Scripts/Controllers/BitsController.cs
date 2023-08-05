using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BitsController : MonoBehaviour
{
    [Inject] private readonly EnemyFactory _enemyFactory;

    [SerializeField] private Bits _prefab;
    [SerializeField] private Transform _container;
    [SerializeField] private Sprite[] _spritesForBits = new Sprite[4];

    private readonly List<Bits> _bitsList = new();
    private readonly int[] _bitsCost = new int[4]
    {
        2,
        4,
        12,
        126
    };

    public void Init()
    {
        CreateBits(30);
    }

    private void OnEnable()
    {
        _enemyFactory.OnEnemyCreated += OnEnemyCreated;
    }

    private void OnDisable()
    {
        _enemyFactory.OnEnemyCreated -= OnEnemyCreated;
    }

    private void OnEnemyCreated(Enemy enemy)
    {
        enemy.OnDead += SpawnBits;
    }

    private void SpawnBits(Enemy enemy)
    {
        Bits bits = FindInactiveBits();

        if(bits == null)
        {
            CreateBits(30);
            bits = FindInactiveBits();
        }

        int enemyLevel = enemy.EnemyLevel;
        bits.UpdateData(enemy.transform.position, _bitsCost[enemyLevel], _spritesForBits[enemyLevel]);
    }

    private Bits FindInactiveBits()
    {
        Bits bits = null;

        for (int i = 0; i < _bitsList.Count; i++)
        {
            if (_bitsList[i].gameObject.activeSelf) continue;
            bits = _bitsList[i];
            break;
        }

        return bits;
    }

    private void CreateBits(int capacity)
    {
        for (int i = 0; i < capacity; i++)
        {
            Bits bits = Instantiate(_prefab, _container);
            bits.Init();
            _bitsList.Add(bits);
        }
    }
}
