using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ShopController : MonoBehaviour
{
    [Inject] private readonly PlayerUnit _player;

    [SerializeField] private Item[] _itemsPrefabs;
    [SerializeField] private PlayerInterface _interface;

    private const int MAX_ITEMS_IN_SHOP = 3;
    private readonly List<Item> _items = new();

    public void Init()
    {
        _player.Experience.OnPlayerGotNewLevel += OpenShop;
        _interface.OnShopClosed += OnShopClosed;
    }

    private void OpenShop()
    {
        _interface.ShowShop();
        ShowCards();
    }

    private void OnShopClosed()
    {
        RemoveItems();

        _player.UpdateProperties();
    }

    private void ShowCards()
    {
        ArrayHandler array = new();
        _itemsPrefabs = array.MixArray(_itemsPrefabs);

        for (int i = 0; i < _itemsPrefabs.Length; i++)
        {
            if (PlayerData.Items.Any(item => item.GetType() == _itemsPrefabs[i].GetType())) continue;

            Item item = Instantiate(_itemsPrefabs[i]);
            ItemCard card = _interface.CreateCard(item);

            _items.Add(item);
            card.OnSelectItem += OnSelectItem;

            if (i >= MAX_ITEMS_IN_SHOP)
            {
                break;
            }
        }
    }

    private void RemoveItems()
    {
        if (_items.Count > 0)
        {
            foreach (Item item in _items)
            {
                Destroy(item.gameObject);
            }
            _items.Clear();
        }
    }

    private void OnSelectItem(Item item, ItemCard card)
    {
        if (!Money.TrySpend(item.GetPrice))
        {
            print("Нет денег чел");
            return;
        }

        item.Use();
        PlayerData.Items.Add(item);

        _interface.DeleteCard(card);
        _interface.UpdatePriceForCards();
    }
}
