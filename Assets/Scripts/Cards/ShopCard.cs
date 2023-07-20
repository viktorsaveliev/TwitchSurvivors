using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ShopCard : MonoBehaviour, IShopCard
{
    [SerializeField] protected TMP_Text NameText;
    [SerializeField] protected TMP_Text Price;
    [SerializeField] protected Image Icon;
    [SerializeField] protected TMP_Text[] PropertiesText;

    public event Action<Item, ShopCard> OnSelectCard;

    protected Button Button;
    protected Item ShopItem;

    private void OnDestroy()
    {
        Button.onClick.RemoveListener(OnSelect);
    }

    public virtual void Init(Item item)
    {
        ShopItem = item;
        InitButton();
    }

    public virtual void UpdateStats()
    {
        NameText.text = ShopItem.GetName();
        Icon.sprite = ShopItem.GetIcon();

        UpdatePrice();
    }

    public void UpdatePrice()
    {
        SetButtonSettings(ShopItem.GetPrice());
    }

    protected void SetButtonSettings(int price)
    {
        if (price <= Money.Value)
        {
            Button.interactable = true;
            Price.text = $"<color=green>{price}$</color>";
        }
        else
        {
            Button.interactable = false;
            Price.text = $"<color=red>{price}$</color>";
        }
    }

    private void InitButton()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(OnSelect);
    }

    private void OnSelect()
    {
        OnSelectCard?.Invoke(ShopItem, this);
    }
}
