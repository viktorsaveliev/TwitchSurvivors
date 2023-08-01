using System.Collections;
using UnityEngine;
using Zenject;
using static PlayerData;

public class PlayerBehaviour : MonoBehaviour
{
    [Inject] private readonly PlayerUnit _playerUnit;
    [Inject] private readonly ItemFactory _itemFactory;
    [Inject] private readonly Notify _notify;

    [Inject] private readonly IEnemyCounter _enemyCounter;

    private Coroutine _attackTimer;
    private readonly float _delayBetweenAttacks = 0.5f;

    private bool _isCanAttack;

    public void Init()
    {
        InitPlayerProperties();

        AddItem<Pistol>();
        //AddItem<BanHammer>();

        switch(SelectedCharacter.Name)
        {
            case "Бустер":
                AddItem<BusterBandage>();
                break;
        }

        _isCanAttack = true;
        _attackTimer = StartCoroutine(StartAttackTimer());

        _notify.Show("Не дай чату убить себя!!!");
    }

    public void DeInit()
    {
        if(_attackTimer != null)
        {
            StopCoroutine(_attackTimer);
            _attackTimer = null;
        }
    }

    private void InitPlayerProperties()
    {
        ResetProperties();

        if (SelectedCharacter == null) return;

        AppendPropertieValue(Properties.Health,         SelectedCharacter.Health);
        AppendPropertieValue(Properties.Regeneration,   SelectedCharacter.Regeneration);
        AppendPropertieValue(Properties.Damage,         SelectedCharacter.Damage);
        AppendPropertieValue(Properties.CriticalDamage, SelectedCharacter.CriticalDamage);
        AppendPropertieValue(Properties.AttackSpeed,    SelectedCharacter.AttackSpeed);
        AppendPropertieValue(Properties.Distance,       SelectedCharacter.Distance);
        AppendPropertieValue(Properties.Armor,          SelectedCharacter.Armor);
        AppendPropertieValue(Properties.Dodge,          SelectedCharacter.Dodge);
        AppendPropertieValue(Properties.MoveSpeed,      SelectedCharacter.MoveSpeed);
        AppendPropertieValue(Properties.Fortune,        SelectedCharacter.Fortune);
        AppendPropertieValue(Properties.Greed,          SelectedCharacter.Greed);

        _playerUnit.UpdateProperties();
        _playerUnit.Health.SetHealth(_playerUnit.Health.MaxValue);
    }

    private void AddItem<T>()
    {
        foreach (Item item in _itemFactory.Items)
        {
            if (item is T == false) continue;

            if (item is IFollower follower)
            {
                follower.SetFollowTarget(_playerUnit.transform);
            }

            item.Use();
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
