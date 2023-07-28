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
            _toggleImage[0].enabled = true;
            _toggleImage[1].enabled = false;
        }
        else
        {
            _toggleImage[0].enabled = false;
            _toggleImage[1].enabled = true;
        }

        _isActive = active;
    }

    private void OnClickButton()
    {
        if (_isActive)
        {
            _toggleImage[0].enabled = true;
            _toggleImage[1].enabled = false;
        }
        else
        {
            _toggleImage[0].enabled = false;
            _toggleImage[1].enabled = true;
        }

        _isActive = !_isActive;
        OnClick?.Invoke(_isActive);
    }
}
