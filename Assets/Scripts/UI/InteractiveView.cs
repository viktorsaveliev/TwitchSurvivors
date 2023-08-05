using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractiveView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Color _selectableColor;

    private Color _originalColor;

    private void Awake()
    {
        _originalColor = _text.color;
    }

    private void OnDisable()
    {
        _text.color = _originalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.DOColor(_selectableColor, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _text.DOColor(_originalColor, 0.5f);
    }
}
