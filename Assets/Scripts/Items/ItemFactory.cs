using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    [SerializeField] private Item[] _itemPrefabs;
    [SerializeField] private Transform _container;

    private readonly List<Item> _shopItems = new();
    public IReadOnlyList<Item> Items => _shopItems;

    public void Init()
    {
        CreateItems();
    }

    public void CreateItems()
    {
        foreach (Item item in _itemPrefabs)
        {
            Item itemObject = Instantiate(item, _container);
            itemObject.gameObject.SetActive(false);
            itemObject.Init();

            _shopItems.Add(itemObject);
        }
    }

    public void RemoveItems()
    {
        if (Items.Count > 0)
        {
            foreach (Item item in Items)
            {
                item.DestroyItem();
            }
        }
    }
}
