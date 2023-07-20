using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PauseHandler
{
    private readonly MonoBehaviour _monoBehaviour;
    private readonly PlayerInterface _interface;

    private readonly float _durationForChangeTimeScale = 0.5f;
    public bool IsPause { get; private set; }

    public PauseHandler(MonoBehaviour monoBehaviour, PlayerInterface playerInterface)
    {
        _monoBehaviour = monoBehaviour;
        _interface = playerInterface;
    }

    public void Init()
    {
        _interface.OnShopOpened += ActivatePause;
        _interface.OnShopClosed += DeactivatePause;
    }

    private void ActivatePause()
    {
        _monoBehaviour.StartCoroutine(ChangeTimeScaleOverTime(0, _durationForChangeTimeScale));
    }

    private void DeactivatePause()
    {
        _monoBehaviour.StartCoroutine(ChangeTimeScaleOverTime(1, _durationForChangeTimeScale));
    }

    private IEnumerator ChangeTimeScaleOverTime(float targetValue, float duration)
    {
        float startTimeScale = Time.timeScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
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
    }
}
