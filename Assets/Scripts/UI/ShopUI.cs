using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopUI : MonoBehaviour
{
    [Inject] private readonly PlayerUnit _player;

    [Header("Player")]
    [SerializeField] private Image _expProgress;
    [SerializeField] private TMP_Text _expText;

    [SerializeField] private Image _hpProgress;
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private TMP_Text _moneyCount;

    [Header("Shop")]
    [SerializeField] private GameObject _cardsView;
    [SerializeField] private Image _shopBG;

    [SerializeField] private ItemCard _itemCardPrefab;
    [SerializeField] private WeaponCard _weaponCardPrefab;

    [SerializeField] private Transform _content;

    [SerializeField] private Button _fightButton;
    [SerializeField] private Button _rerollButton;
    [SerializeField] private TMP_Text _moneyText;

    private readonly List<ShopCard> _cards = new();

    public event Action OnShopOpened;
    public event Action OnShopClosed;
    public event Action OnClickRerollButton;

    private bool _isActive;
    public bool IsActive => _isActive;

    public void Init()
    {
        _player.Experience.OnExpValueChanged += UpdateExpUI;
        _player.Health.OnHealthChanged += UpdateHealthUI;
        _player.Health.OnTakedDamage += UpdateHealthUI;
        _player.OnPickupBits += UpdateMoney;

        _fightButton.onClick.AddListener(HideShop);
        _rerollButton.onClick.AddListener(Reroll);

        UpdateHealthUI();
        UpdateExpUI();
        UpdateMoney();
    }

    public void ShowShop()
    {
        _moneyText.text = $"{Money.Value}";

        _cardsView.SetActive(true);
        _shopBG.gameObject.SetActive(true);
        _shopBG.DOFade(0.99f, 0.2f).SetUpdate(true);

        _isActive = true;

        OnShopOpened?.Invoke();
    }

    public ShopCard CreateCard(Item item)
    {
        ShopCard card = (ShopCard) Instantiate(item is PropertyItem ? _itemCardPrefab : _weaponCardPrefab, _content);
        card.Init(item);

        card.transform.localScale = Vector2.zero;
        card.transform.DOScale(1, 0.4f).SetUpdate(true).SetEase(Ease.OutBack);

        _cards.Add(card);

        return card;
    }

    public void UpdatePriceForCards()
    {
        _moneyText.text = $"{Money.Value}";

        foreach (ShopCard card in _cards)
        {
            card.UpdatePrice();
        }
    }

    public void DeleteCard(ShopCard card)
    {
        _cards.Remove(card);

        HideOtherCards();

        card.transform.DOScale(0, 0.2f).SetUpdate(true).SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                Destroy(card.gameObject);
                ShowOtherCards();
            });
    }

    public void DeleteAllCards()
    {
        foreach (ShopCard card in _cards)
        {
            Destroy(card.gameObject);
        }
        _cards.Clear();
    }

    private void HideOtherCards()
    {
        foreach (ShopCard card in _cards)
        {
            card.transform.DOScaleX(0, 0.05f).SetUpdate(true);
        }
    }

    private void ShowOtherCards()
    {
        foreach (ShopCard card in _cards)
        {
            card.transform.DOScaleX(1, 0.05f).SetUpdate(true);
        }
    }

    private void Reroll()
    {
        OnClickRerollButton?.Invoke();
    }

    private void UpdateMoney()
    {
        _moneyCount.text = $"{Money.Value}";
    }

    private void UpdateExpUI()
    {
        _expText.text = $"LVL {_player.Experience.Level}";

        float percentage = _player.Experience.Value / (float)_player.Experience.ExpForNewLevel;
        _expProgress.fillAmount = percentage;
    }

    private void UpdateHealthUI(int damage) => UpdateHealthUI();

    private void UpdateHealthUI()
    {
        _hpText.text = $"{_player.Health.CurrentValue}/{_player.Health.MaxValue}";

        float percentage = _player.Health.CurrentValue / (float)_player.Health.MaxValue;
        _hpProgress.fillAmount = percentage;
    }

    private void HideShop()
    {
        _shopBG.DOFade(0, 0.2f).SetUpdate(true)
            .OnComplete(() => _shopBG.gameObject.SetActive(false));

        _cardsView.SetActive(false);

        if (_cards.Count > 0)
        {
            foreach (ShopCard card in _cards)
            {
                Destroy(card.gameObject);
            }

            _cards.Clear();
        }

        _isActive = false;

        UpdateMoney();
        OnShopClosed?.Invoke();
    }
}
