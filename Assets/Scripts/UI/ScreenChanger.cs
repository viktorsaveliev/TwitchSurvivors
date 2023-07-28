using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ScreenChanger : MonoBehaviour
{
    [SerializeField] private GameObject _currentScreen;
    [SerializeField] private GameObject _nextScreen;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(GoToNextScreen);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(GoToNextScreen);
    }

    public void SetNextScreen(GameObject screen)
    {
        _nextScreen = screen;
    }

    private void GoToNextScreen()
    {
        if (_currentScreen != null)
            _currentScreen.SetActive(false);

        if(_nextScreen != null) 
            _nextScreen.SetActive(true);
    }
}
