using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public abstract class ShopCard : MonoBehaviour, IShopCard, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected TMP_Text NameText;
    [SerializeField] protected TMP_Text Price;
    [SerializeField] protected Image Icon;
    [SerializeField] protected TMP_Text[] PropertiesText;

    [SerializeField] private Image[] _backgrounds;

    public event Action<Item, ShopCard> OnSelectCard;

    protected Button Button;
    protected Item ShopItem;

    private Color _currentColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Button.interactable) return;

        _currentColor = _backgrounds[0].color;
        SetColor(new Color(0.9f, 0.3f, 0.9f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetColor(_currentColor);
    }

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
        SetInteractable(ShopItem.GetPrice());
    }

    protected void SetInteractable(int price)
    {
        if (price <= Money.Value)
        {
            Button.interactable = true;
            Price.text = $"{price}";
        }
        else
        {
            Button.interactable = false;
            Price.text = $"<color=#D7424B>{price}</color>";
        }
    }

    protected void SetColor(Color color)
    {
        foreach (Image bg in _backgrounds)
        {
            bg.color = color;
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
