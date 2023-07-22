using System.Linq;
using UnityEngine;

public class Shop
{
    private readonly PlayerUnit _player;
    private readonly ItemFactory _itemFactory;
    private readonly PlayerInterface _interface;
    
    private const int MAX_ITEMS_IN_SHOP = 5;

    public Shop(PlayerUnit player, ItemFactory itemFactory, PlayerInterface playerInterface)
    {
        _player = player;
        _itemFactory = itemFactory;
        _interface = playerInterface;
    }

    public void Init()
    {
        _player.Experience.OnPlayerGotNewLevel += OpenShop;
        _interface.OnShopClosed += OnShopClosed;
    }

    private void OpenShop()
    {
        _interface.ShowShop();
        ShowRandomItems();
    }

    private void OnShopClosed()
    {
        _player.UpdateProperties();
    }

    private void ShowRandomItems()
    {
        ArrayHandler array = new();

        Item[] itemArray = _itemFactory.Items.ToArray();
        itemArray = array.MixArray(itemArray);

        int full = 0;
        for (int i = 0; i < itemArray.Length; i++)
        {
            if (full >= MAX_ITEMS_IN_SHOP)
            {
                break;
            }

            if (_player.Inventory.HasItem(itemArray[i]))
            {
                if (itemArray[i] is PropertyItem) continue;
                else if (itemArray[i] is Weapon weapon && weapon.ImprovementLevel > 3) continue;
            }

            ShopCard card = _interface.CreateCard(itemArray[i]);
            card.OnSelectCard += OnSelectItem;

            full++;
        }
    }

    private void OnSelectItem(Item item, ShopCard card)
    {
        if (!Money.TrySpend(item.GetPrice()))
        {
            Debug.Log("Нет денег чел");
            return;
        }

        item.Use();

        if (item is IFollower follower)
        {
            follower.SetFollowTarget(_player.transform);
        }

        _player.Inventory.AddItem(item);

        _interface.DeleteCard(card);
        _interface.UpdatePriceForCards();
    }
}