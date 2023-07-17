using UnityEngine;
using System.Collections;
using System;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class Timer : MonoBehaviour
{
    private TMP_Text _timerText;

    private readonly float _timerDuration = 1f;
    private bool _isPause;
    private float _currentPlayTime;

    private void Start()
    {
        _timerText = GetComponent<TMP_Text>();
        _isPause = false;

        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        WaitForSeconds second = new(_timerDuration);
        while (!_isPause)
        {
            yield return second;
            _currentPlayTime++;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(_currentPlayTime);
        string formattedTime = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);

        _timerText.text = formattedTime;
    }
}