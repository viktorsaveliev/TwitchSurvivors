using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;

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

    private InteractiveSound _sound;
    private bool _onSelect;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Button.interactable) return;

        transform.DOScale(1.1f, 0.2f).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!Button.interactable || _onSelect) return;
        transform.DOScale(1, 0.2f).SetUpdate(true);
    }

    private void OnDestroy()
    {
        Button.onClick.RemoveListener(OnSelect);
    }

    public virtual void Init(Item item)
    {
        _sound = GetComponent<InteractiveSound>();
        ShopItem = item;
        InitButton();
    }

    public virtual void UpdateStats()
    {
        NameText.text = ShopItem.GetName();
        Icon.sprite = ShopItem.GetIcon();
        Icon.SetNativeSize();
        _onSelect = false;

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
            if (_sound != null)
            {
                _sound.enabled = true;
            }

            Button.interactable = true;
            Price.text = $"{price}";
        }
        else
        {
            if (_sound != null)
            {
                _sound.enabled = false;
            }

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
        _onSelect = true;
        OnSelectCard?.Invoke(ShopItem, this);
    }
}
