using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SettingsSlider : MonoBehaviour
{
    [SerializeField] private TMP_Text _percentsText;

    public event Action<float> OnValidate;

    private Slider _slider;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    public void Init()
    {
        _slider = GetComponent<Slider>();
    }

    public void SetValue(float value)
    {
        _slider.value = value;
        _percentsText.text = $"{value * 100:F0}%";
    }

    private void OnValueChanged(float value)
    {
        _percentsText.text = $"{value * 100:F0}%";
        OnValidate?.Invoke(value);
    }
}
