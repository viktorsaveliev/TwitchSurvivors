using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SettingsButton : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ShowSettings);
    }

    private void OnDisable()
    {
        _button.onClick.AddListener(ShowSettings);
    }

    private void ShowSettings()
    {
        _menu.SetActive(false);

        SettingsUI.Show();
        SettingsUI.ActivateScreenAfterClose(_menu);
    }
}
