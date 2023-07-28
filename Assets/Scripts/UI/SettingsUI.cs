using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private GameObject _settingsUI;
    [SerializeField] private Button _closeButton;

    private static GameObject _settings;
    private static ScreenChanger _changeScreen;

    private void Awake()
    {
        _settings = _settingsUI;
        _changeScreen = _closeButton.GetComponent<ScreenChanger>();
    }

    public static void Show() => _settings.SetActive(true);

    public static void Hide() => _settings.SetActive(false);

    public static void ActivateScreenAfterClose(GameObject screen) => _changeScreen.SetNextScreen(screen);
}
