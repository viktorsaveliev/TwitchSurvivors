using System;
using System.Collections.Generic;

public class UnitInventory
{
    private readonly List<Item> _items = new();
    public IReadOnlyList<Item> Items => _items;

    public event Action<Item> OnAddedNewItem;
    public event Action<Item> OnRemovedItem;

    public void AddItem(Item item)
    {
        if (!_items.Contains(item))
        {
            _items.Add(item);

            OnAddedNewItem?.Invoke(item);
        }
        else
        {
            if (item is Weapon weapon)
            {
                weapon.Improve();
            }
        }
    }

    public void RemoveItem(Item item)
    {
        if (_items.Contains(item))
        {
            _items.Remove(item);
            item.gameObject.SetActive(false);

            OnRemovedItem?.Invoke(item);
        }
    }

    public bool HasItem(Item item)
    {
        return _items.Contains(item);
    }
}
