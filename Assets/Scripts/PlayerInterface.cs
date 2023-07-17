using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerInterface : MonoBehaviour
{
    [Inject] private readonly PlayerUnit _player;

    [Header("Player experience")]
    [SerializeField] private Image _expProgress;
    [SerializeField] private TMP_Text _expText;

    [Header("Shop")]
    [SerializeField] private GameObject _cardsView;
    [SerializeField] private ItemCard _cardPrefab;
    [SerializeField] private Transform _content;
    [SerializeField] private Button _fightButton;

    private readonly List<ItemCard> _cards = new();

    public event Action OnShopClosed;

    public void Init()
    {
        _player.Experience.OnExpValueChanged += UpdateExpUI;
        _fightButton.onClick.AddListener(HideShop);
    }

    public void ShowShop()
    {
        _cardsView.SetActive(true);
    }

    public ItemCard CreateCard(Item item)
    {
        ItemCard card = Instantiate(_cardPrefab, _content);
        card.Init(item);
        _cards.Add(card);

        return card;
    }

    private void UpdateExpUI()
    {
        _expText.text = $"{_player.Experience.Exp} / {_player.Experience.ExpForNewLevel}";

        float percentage = _player.Experience.Exp / (float)_player.Experience.ExpForNewLevel;
        _expProgress.fillAmount = percentage;
    }

    public void UpdatePriceForCards()
    {
        foreach (ItemCard card in _cards)
        {
            card.UpdatePrice();
        }
    }

    public void DeleteCard(ItemCard card)
    {
        _cards.Remove(card);
        Destroy(card.gameObject);
    }

    private void HideShop()
    {
        _cardsView.SetActive(false);

        if (_cards.Count > 0)
        {
            foreach (ItemCard card in _cards)
            {
                Destroy(card.gameObject);
            }

            _cards.Clear();
        }
 
        OnShopClosed?.Invoke();
    }
}
