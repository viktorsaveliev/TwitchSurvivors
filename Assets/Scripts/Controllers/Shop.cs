using System;
using System.Linq;
using UnityEngine;

public class Shop : IOpenedMenu
{
    public event Action OnOpened;
    public event Action OnClosed;

    private readonly PlayerUnit _player;
    private readonly ItemFactory _itemFactory;
    private readonly ShopUI _interface;
    private readonly AudioController _audio;

    private const int MAX_ITEMS_IN_SHOP = 5;
    private int _priceForReroll = 10;

    public Shop(PlayerUnit player, ItemFactory itemFactory, ShopUI playerInterface, AudioController audio)
    {
        _player = player;
        _itemFactory = itemFactory;
        _interface = playerInterface;
        _audio = audio;
    }

    public void Init()
    {
        _player.Experience.OnPlayerGotNewLevel += OpenShop;

        _interface.OnShopClosed += CloseShop;
        _interface.OnClickRerollButton += RerollItems;
    }

    private void OpenShop()
    {
        Cursor.visible = true;

        _interface.ShowShop();
        _interface.UpdateRerollPrice(_priceForReroll);

        ShowRandomItems();
        OnOpened?.Invoke();
    }

    private void CloseShop()
    {
        Cursor.visible = false;

        _player.UpdateProperties();
        OnClosed?.Invoke();
    }

    private void ShowRandomItems()
    {
        ArrayHandler array = new();

        Item[] itemArray = _itemFactory.Items.ToArray();
        itemArray = array.MixArray(itemArray);

        int full = 0;

        foreach (Item item in itemArray)
        {
            if (full >= MAX_ITEMS_IN_SHOP) break;

            bool isHaveItem = _player.Inventory.HasItem(item);
            if (item is Weapon cweapon
                && _player.Weapons.Count == UnitWeapons.MAX_WEAPONS 
                && !isHaveItem) 
                continue;

            if (isHaveItem)
            {
                if (item is PropertyItem) continue;
                else if (item is Weapon weapon && weapon.ImprovementLevel > 3) continue;
            }

            ShopCard card = _interface.CreateCard(item);
            card.OnSelectCard += OnSelectItem;

            full++;
        }
    }

    private void RerollItems()
    {
        if (!Money.TrySpend(_priceForReroll))
        {
            return;
        }

        _interface.DeleteAllCards();
        ShowRandomItems();

        _priceForReroll += 10;
        _interface.UpdateRerollPrice(_priceForReroll);
    }

    private void OnSelectItem(Item item, ShopCard card)
    {
        if (item is Weapon
                && _player.Weapons.Count == UnitWeapons.MAX_WEAPONS
                && !_player.Inventory.HasItem(item))
            return;

        if (!Money.TrySpend(item.GetPrice()))
        {
            Debug.Log("Нет денег чел");
            return;
        }

        if (item is SkillzorRap rap && rap.ImprovementLevel < 0)
        {
            _audio.StopMusic();
            _audio.PlaySkillzorRap();
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