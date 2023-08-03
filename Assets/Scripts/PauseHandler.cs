using UnityEngine;
using System.Collections;
using System;

public class PauseHandler
{
    public event Action OnPauseActive;
    public event Action OnPauseDeactive;
    
    private readonly MonoBehaviour _monoBehaviour;
    private readonly ShopUI _shopUI;
    private readonly PauseMenu _pauseMenu;

    private readonly IInputControl _input;

    private readonly float _durationForChangeTimeScale = 0.5f;
    private Coroutine _coroutine;

    public bool IsPause { get; private set; }

    public PauseHandler(MonoBehaviour monoBehaviour, ShopUI playerInterface, IInputControl input, PauseMenu pauseMenu)
    {
        _monoBehaviour = monoBehaviour;
        _shopUI = playerInterface;
        _input = input;
        _pauseMenu = pauseMenu;
    }

    public void Init()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        IsPause = false;

        _shopUI.OnShopOpened += ActivatePause;
        _shopUI.OnShopClosed += DeactivatePause;

        _pauseMenu.OnClickResumeButton += DeactivatePause;
        _pauseMenu.OnMenuHide += DeactivatePause;

        _input.OnCallPauseMenu += SwitchPauseState;
    }

    private void SwitchPauseState()
    {
        if (IsPause) DeactivatePause();
        else ActivatePause();
    }

    private void ActivatePause()
    {
        if (_coroutine != null)
        {
            _monoBehaviour.StopCoroutine(_coroutine);
        }

        Time.timeScale = 0;
        Time.fixedDeltaTime = 0f;
        IsPause = true;

        //_monoBehaviour.StartCoroutine(ChangeTimeScaleOverTime(0, _durationForChangeTimeScale));
        OnPauseActive?.Invoke();
    }

    private void DeactivatePause()
    {
        if (_pauseMenu.IsActive || _shopUI.IsActive) return;

        if (_coroutine != null)
        {
            _monoBehaviour.StopCoroutine(_coroutine);
        }

        _coroutine = _monoBehaviour.StartCoroutine(ChangeTimeScaleOverTime(1, _durationForChangeTimeScale));
        OnPauseDeactive?.Invoke();
    }

    private IEnumerator ChangeTimeScaleOverTime(float targetValue, float duration)
    {
        float startTimeScale = Time.timeScale;
        float elapsedTime = 0f;

        while (_coroutine != null && elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(startTimeScale, targetValue, elapsedTime / duration);
            yield return null;
        }

        Time.timeScale = targetValue;

        if (Time.timeScale <= 0.01f)
        {
            Time.fixedDeltaTime = 0f;
            IsPause = true;
        }
        else if (Time.timeScale >= 0.99f) 
        {
            Time.fixedDeltaTime = 0.02f;
            IsPause = false;
        }

        _coroutine = null;
    }
}
