using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWeapons
{
    public event Action OnWeaponsHiden;

    public const int MAX_WEAPONS = 6;
    public int Count => _weapons.Count;

    private readonly List<Weapon> _weapons = new();

    private readonly MonoBehaviour _monoBehaviour;
    private readonly float _delayBetweenAnotherWeapons = 0.1f;
    private readonly Transform[] _weaponPositions;
    private readonly GameObject _unitGameObject;

    private float _hideWeaponTime;

    public UnitWeapons(MonoBehaviour monoBehaviour, Transform[] weaponPositions)
    {
        _monoBehaviour = monoBehaviour;
        _weaponPositions = weaponPositions;
        _unitGameObject = monoBehaviour.gameObject;
    }

    public void Equip(Weapon weapon)
    {
        weapon.gameObject.SetActive(true);
        _weapons.Add(weapon);

        if (weapon.IsVisible)
        {
            weapon.transform.position = _weaponPositions[_weapons.Count - 1].position;
        }
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
        if (_hideWeaponTime <= Time.time)
        {
            if (_hideWeaponTime != 0)
            {
                ShowWeapons();
            }

            if (_unitGameObject.activeSelf)
            {
                _monoBehaviour.StartCoroutine(AttackWithDelay(enemyDetection));
            }
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

    private void ShowWeapons()
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            _weapons[i].gameObject.SetActive(true);
            _weapons[i].ResetBehaviour();
            _hideWeaponTime = 0;
        }
    }

    public void HideWeapons(float time)
    {
        if (_hideWeaponTime != 0) return;

        for (int i = 0; i < _weapons.Count; i++)
        {
            _weapons[i].gameObject.SetActive(false);
        }
        _hideWeaponTime = Time.time + time;

        OnWeaponsHiden?.Invoke();
    }
}
