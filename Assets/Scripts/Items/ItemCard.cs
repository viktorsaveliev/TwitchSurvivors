using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemCard : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text[] _propertiesText;

    private Button _button;
    private Item _item;

    public event Action<Item, ItemCard> OnSelectItem;

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnSelect);
    }

    public void Init(Item item)
    {
        _button = GetComponent<Button>();
        _item = item;
        _button.onClick.AddListener(OnSelect);

        UpdateStats(item);
    }

    private void OnSelect()
    {
        OnSelectItem?.Invoke(_item, this);
    }

    private void UpdateStats(Item item)
    {
        _nameText.text = item.GetItemName;
        _icon.sprite = item.SpriteIcon;

        UpdatePrice();

        int index = 0;
        foreach (var pair in item.Properties)
        {
            string percent = pair.Value > 0 ? $"<color=green>+{pair.Value}</color>" : $"<color=red>{pair.Value}</color>";
            _propertiesText[index].text = $"{PlayerData.PropertiesName[(int)pair.Key]}: {percent}%";
            index++;
        }

        for (int i = index; i < _propertiesText.Length; i++)
        {
            _propertiesText[i].text = string.Empty;
        }
    }

    public void UpdatePrice()
    {
        if (_item.GetPrice <= Money.Value)
        {
            _button.interactable = true;
            _price.text = $"<color=green>{_item.GetPrice}$</color>";
        }
        else
        {
            _button.interactable = false;
            _price.text = $"<color=red>{_item.GetPrice}$</color>";
        }
    }
}
