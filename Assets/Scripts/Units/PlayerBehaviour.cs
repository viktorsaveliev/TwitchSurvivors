using System.Collections;
using System.Linq;
using UnityEngine;
using Zenject;

public class PlayerBehaviour : MonoBehaviour
{
    [Inject] private readonly PlayerUnit _playerUnit;
    [Inject] private readonly ItemFactory _itemFactory;

    [Inject] private readonly IEnemyCounter _enemyCounter;

    private Coroutine _attackTimer;
    private readonly float _delayBetweenAttacks = 0.5f;

    private bool _isCanAttack;

    public void Init()
    {
        AddRandomWeapon();

        _isCanAttack = true;
        _attackTimer = StartCoroutine(StartAttackTimer());
    }

    public void DeInit()
    {
        if(_attackTimer != null)
        {
            StopCoroutine(_attackTimer);
            _attackTimer = null;
        }
    }
    
    private void AddRandomWeapon()
    {
        Item[] items = _itemFactory.Items.ToArray();
        ArrayHandler array = new();
        items = array.MixArray(items);

        foreach (Item item in items)
        {
            if (item is Zeus == false) continue;
            _playerUnit.Inventory.AddItem(item);

            break;
        }
    }

    private IEnumerator StartAttackTimer()
    {
        WaitForSeconds delayBetweenAttacks = new(_delayBetweenAttacks);

        while(_isCanAttack)
        {
            _playerUnit.Weapons.Attack(_enemyCounter);
            yield return delayBetweenAttacks;
        }
    }
}
