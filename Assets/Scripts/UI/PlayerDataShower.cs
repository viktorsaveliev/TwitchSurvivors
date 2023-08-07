using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerDataShower : MonoBehaviour
{
    [Inject] private readonly PlayerUnit _player;

    [SerializeField] private Transform _itemsContent;
    [SerializeField] private Image _itemIconPrefab;
    [SerializeField] private PropertySlot[] _propertySlots;
    [SerializeField] private WeaponSlot[] _weaponSlots;
    
    private readonly List<Image> _itemIcons = new();
    private bool _updateItems;

    public void Init(IOpenedMenu menu, bool updateItems)
    {
        menu.OnOpened += ShowAvailableItemsIcon;
        menu.OnClosed += DeleteAvailableItemsIcon;
        _updateItems = updateItems;

        _player.Inventory.OnAddedNewItem += UpdateData;
    }

    private void ShowAvailableItemsIcon()
    {
        foreach (var item in _player.Inventory.Items)
        {
            if (item is Weapon) continue;
            Image icon = Instantiate(_itemIconPrefab, _itemsContent);
            icon.sprite = item.GetIcon();

            _itemIcons.Add(icon);
        }
    }

    private void DeleteAvailableItemsIcon()
    {
        if (_itemIcons.Count == 0) return;

        foreach (var item in _itemIcons)
        {
            Destroy(item.gameObject);
        }

        _itemIcons.Clear();
    }

    private void UpdateData(Item item)
    {
        if (_updateItems && item is PropertyItem)
        {
            Image icon = Instantiate(_itemIconPrefab, _itemsContent);
            icon.sprite = item.GetIcon();
            _itemIcons.Add(icon);

            foreach (PropertySlot propertie in _propertySlots)
            {
                propertie.UpdatePropertie();
            }
        }
        else if (item is Weapon)
        {
            foreach (WeaponSlot weaponSlot in _weaponSlots)
            {
                if (weaponSlot.IsUsed) continue;
                weaponSlot.SetIcon(item.GetIcon());
                break;
            }
        }
    }
}
