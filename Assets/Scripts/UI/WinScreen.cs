using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour, IOpenedMenu
{
    [SerializeField] private GameObject _screen;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private AudioSource _soundFX;

    public event Action OnOpened;
    public event Action OnClosed;

    private AudioController _audio;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(RestartGame);
        _menuButton.onClick.AddListener(GoToMenu);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(RestartGame);
        _menuButton.onClick.RemoveListener(GoToMenu);
    }

    public void Init(EnemySpawner spawner, AudioController audio)
    {
        spawner.OnEnemySpawned += SubOnBossDead;
        _audio = audio;
    }

    public void Hide()
    {
        Cursor.visible = false;
        _screen.SetActive(false);
        OnClosed?.Invoke();
    }

    private void ShowWinnerScreen(Enemy enemy)
    {
        enemy.OnDead -= ShowWinnerScreen;
        StartCoroutine(Show());
    }

    private IEnumerator Show()
    {
        yield return new WaitForSeconds(1f);
        Cursor.visible = true;
        _screen.SetActive(true);
        _audio.StopMusic();
        _soundFX.Play();

        OnOpened?.Invoke();
    }

    private void SubOnBossDead(Enemy enemy)
    {
        if (enemy is WhiteRa whiteRa)
        {
            whiteRa.OnDead += ShowWinnerScreen;
        }
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
