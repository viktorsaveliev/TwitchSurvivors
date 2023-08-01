using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoseScreen : MonoBehaviour
{
    [Inject] private readonly PlayerUnit _player;

    [SerializeField] private GameObject _screen;
    [SerializeField] private TMP_Text _killerNickname;

    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;

    private void OnEnable()
    {
        _player.Health.OnHealthOver += Show;

        _restartButton.onClick.AddListener(RestartGame);
        _menuButton.onClick.AddListener(GoToMenu);
    }

    private void OnDisable()
    {
        _player.Health.OnHealthOver -= Show;

        _restartButton.onClick.RemoveListener(RestartGame);
        _menuButton.onClick.RemoveListener(GoToMenu);
    }

    public void Show()
    {
        string nickname = _player.LastHittedEnemyNickname ?? "Анон";
        _killerNickname.text = nickname;
        _screen.SetActive(true);
    }

    public void Hide()
    {
        _screen.SetActive(false);
    }

    private void RestartGame()
    {
        Hide();

        SceneLoader loader = new();
        loader.LoadLevel();
    }

    private void GoToMenu()
    {
        Hide();

        SceneLoader loader = new();
        loader.LoadMenu();
    }
}
