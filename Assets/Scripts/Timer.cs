using UnityEngine;
using System.Collections;
using System;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class Timer : MonoBehaviour, ITimerSubject
{
    private ITimerObserver _observer;

    private TMP_Text _timerText;

    private readonly float _timerDuration = 1f;
    private readonly float _secondsCounterForRegeneration = 30;
    
    private bool _isPause;
    private float _currentPlayTime;
    private float _secondsCounter;

    public void Init(ITimerObserver observer)
    {
        _timerText = GetComponent<TMP_Text>();
        _isPause = false;
        _observer = observer;

        StartCoroutine(UpdateTimer());
    }

    public void Notify()
    {
        _observer.OnTimerReachedValue();
    }

    private IEnumerator UpdateTimer()
    {
        WaitForSeconds second = new(_timerDuration);
        while (!_isPause)
        {
            yield return second;
            _currentPlayTime++;
            UpdateUI();

            if (++_secondsCounter >= _secondsCounterForRegeneration)
            {
                _secondsCounter = 0;
                Notify();
            }
        }
    }

    private void UpdateUI()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(_currentPlayTime);
        string formattedTime = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);

        _timerText.text = formattedTime;
    }
}