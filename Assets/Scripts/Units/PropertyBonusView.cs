using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class PropertyBonusView : MonoBehaviour
{
    [Inject] private readonly PlayerUnit _unit;

    [SerializeField] private TMP_Text _text;
    [SerializeField] private Color _dodgeColor;
    [SerializeField] private Color _critColor;

    private Transform _transform;

    private void Awake()
    {
        _transform = _text.transform;
    }

    private void OnEnable()
    {
        _unit.OnDodgedDamage += OnDodgedDamage;
    }

    private void OnDisable()
    {
        _unit.OnDodgedDamage -= OnDodgedDamage;
    }

    //private void OnCriticalDamage() => ShowText("Крит. урон", _critColor);

    private void OnDodgedDamage() => ShowText("Уклонение", _dodgeColor);

    private void ShowText(string text, Color color)
    {
        _text.text = text;
        _text.color = color;

        _transform.localScale = Vector2.zero;
        _text.gameObject.SetActive(true);

        _transform.DOScale(1, 1f).SetEase(Ease.OutBounce).OnComplete(HideText);
    }

    private void HideText()
    {
        _transform.DOScale(0, 0.5f)
            .OnComplete(() => _text.gameObject.SetActive(false));
    }
}
