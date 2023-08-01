using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SettingsToggleButton : MonoBehaviour
{
    public event Action<bool> OnClick;

    [SerializeField] private Image[] _toggleImage;
    [SerializeField] private bool _isActive;

    private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClickButton);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClickButton);
    }

    public void Init()
    {
        _button = GetComponent<Button>();
    }

    public void SetActive(bool active)
    {
        if (!active)
        {
            _toggleImage[0].gameObject.SetActive(true);
            _toggleImage[1].gameObject.SetActive(false);
        }
        else
        {
            _toggleImage[0].gameObject.SetActive(false);
            _toggleImage[1].gameObject.SetActive(true);
        }

        _isActive = active;
    }

    private void OnClickButton()
    {
        _isActive = !_isActive;
        SetActive(_isActive);

        OnClick?.Invoke(_isActive);
    }
}
