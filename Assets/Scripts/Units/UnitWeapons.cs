using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWeapons
{
    private readonly List<Weapon> _weapons = new();

    private readonly MonoBehaviour _monoBehaviour;
    private readonly float _delayBetweenAnotherWeapons = 0.1f;

    public UnitWeapons(MonoBehaviour monoBehaviour)
    {
        _monoBehaviour = monoBehaviour;
    }

    public void Equip(Weapon weapon)
    {
        weapon.gameObject.SetActive(true);
        _weapons.Add(weapon);
    }

    public void RemoveWeapon(Weapon weapon)
    {
        if (_weapons.Contains(weapon))
        {
            _weapons.Remove(weapon);
        }
    }

    public void Attack(IEnemyCounter enemyDetection)
    {
        if (_monoBehaviour.gameObject.activeSelf)
        {
            _monoBehaviour.StartCoroutine(AttackWithDelay(enemyDetection));
        }
    }

    private IEnumerator AttackWithDelay(IEnemyCounter enemyDetection)
    {
        WaitForSeconds delayBetweenAnotherWeapons = new(_delayBetweenAnotherWeapons);

        for (int i = 0; i < _weapons.Count; i++)
        {
            if (_weapons[i] is IChargesUser user && !_weapons[i].IsActive)
            {
                user.Shoot(enemyDetection);
            }

            yield return delayBetweenAnotherWeapons;
        }
    }
}
