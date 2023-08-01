using TMPro;
using UnityEngine;

public class PropertySlot : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
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

}
