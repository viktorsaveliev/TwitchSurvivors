using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PropertySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text _text;

    [SerializeField] private GameObject _description;
    [SerializeField] private TMP_Text _descriptionText;

    [SerializeField] private PlayerData.Properties _propertie;

    public PlayerData.Properties Propertie => _propertie;

    private int _currentValue = 0;

    private void OnEnable()
    {
        UpdatePropertie();
    }

    public void UpdatePropertie()
    {
        _currentValue = PlayerData.GetPropertieValue(_propertie);
        _text.text = $"{_currentValue}";
        /*DOTween.To(() => _currentValue, text => _text.text = text.ToString(), value, 0.5f)
            .SetEase(Ease.Linear)
            .OnComplete(() => _currentValue = value);*/
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _descriptionText.text = $"{PlayerData.PropertiesName[(int)_propertie]}";
        _description.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _description.SetActive(false);
    }
}
