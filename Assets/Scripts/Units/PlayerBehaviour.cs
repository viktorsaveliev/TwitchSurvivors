using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerBehaviour : MonoBehaviour
{
    [Inject] private readonly PlayerUnit _playerUnit;
    [Inject] private readonly IEnemyDetection _enemyDetection;

    [SerializeField] private Weapon _weapon;

    private Coroutine _attackTimer;
    private float _delayBetweenAttacks = 2f;

    private bool _isCanAttack;

    public void Init()
    {
        _playerUnit.Weapons.AddWeapon(_weapon);

        _isCanAttack = true;
        _attackTimer = StartCoroutine(StartAttackTimer());
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
