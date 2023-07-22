using DG.Tweening;
using UnityEngine;

public class AudioController
{
    private readonly AudioLowPassFilter _audioFilter;
    private readonly PauseHandler _pauseHandler;

    private readonly float _defaultValue = 10000f;
    private readonly float _lowPassValue = 100f;

    public AudioController(AudioLowPassFilter audioFilter, PauseHandler pauseHandler)
    {
        _audioFilter = audioFilter;
        _pauseHandler = pauseHandler;
    }

    public void Init()
    {
        _pauseHandler.OnPauseActive += ApplyMutedEffect;
        _pauseHandler.OnPauseDeactive += DisableMutedEffect;
    }

    private void ApplyMutedEffect()
    {
        DOTween.To(() => _audioFilter.cutoffFrequency, value => _audioFilter.cutoffFrequency = value, _lowPassValue, 1f)
            .SetEase(Ease.Linear);

    }

    private void DisableMutedEffect()
    {
        DOTween.To(() => _audioFilter.cutoffFrequency, value => _audioFilter.cutoffFrequency = value, _defaultValue, 1f)
            .SetEase(Ease.Linear);
    }
}
