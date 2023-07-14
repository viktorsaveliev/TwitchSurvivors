using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWeapons
{
    private readonly MonoBehaviour _monoBehaviour;

    private readonly List<Weapon> _weapons = new();
    private readonly float _delayBetweenAnotherWeapons = 0.2f;

    public UnitWeapons(MonoBehaviour monoBehaviour)
    {
        _monoBehaviour = monoBehaviour;
    }

    public void AddWeapon(Weapon weapon)
    {
        weapon.Init();
        _weapons.Add(weapon);
    }

    public void RemoveWeapon(Weapon weapon)
    {
        if(_weapons.Contains(weapon))
            _weapons.Remove(weapon);
    }

    public void Attack(IEnemyCounter enemyDetection)
    {
        _monoBehaviour.StartCoroutine(AttackWithDelay(enemyDetection));
    }

    private IEnumerator AttackWithDelay(IEnemyCounter enemyDetection)
    {
        WaitForSeconds delayBetweenAnotherWeapons = new(_delayBetweenAnotherWeapons);

        foreach(Weapon weapon in _weapons)
        {
            weapon.Shoot(enemyDetection);
            yield return delayBetweenAnotherWeapons;
        }
    }
}
