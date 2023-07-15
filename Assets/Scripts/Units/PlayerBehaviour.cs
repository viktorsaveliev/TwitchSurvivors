using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerBehaviour : MonoBehaviour
{
    [Inject] private readonly PlayerUnit _playerUnit;
    [Inject] private readonly IEnemyCounter _enemyDetection;

    [SerializeField] private Weapon[] _weapons;

    private Coroutine _attackTimer;
    private readonly float _delayBetweenAttacks = 1f;

    private bool _isCanAttack;

    public void Init()
    {
        foreach(Weapon weapon in _weapons)
        {
            Weapon newWeapon = Instantiate(weapon, _playerUnit.transform);
            _playerUnit.Weapons.AddWeapon(newWeapon);
        }

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
    
    private IEnumerator StartAttackTimer()
    {
        WaitForSeconds delayBetweenAttacks = new(_delayBetweenAttacks);

        while(_isCanAttack)
        {
            _playerUnit.Weapons.Attack(_enemyDetection);
            yield return delayBetweenAttacks;
        }
    }
}
