using DG.Tweening;
using UnityEngine;

public class AudioController
{
    private readonly AudioSource _soundtrack;
    private readonly AudioLowPassFilter _audioFilter;
    private readonly PauseHandler _pauseHandler;

    private readonly float _defaultValue = 10000f;
    private readonly float _lowPassValue = 600f;

    public AudioController(AudioSource audioSource, AudioLowPassFilter audioFilter, PauseHandler pauseHandler)
    {
        _soundtrack = audioSource;
        _audioFilter = audioFilter;
        _pauseHandler = pauseHandler;
    }

    public void Init()
    {
        _pauseHandler.OnPauseActive += ApplyMutedEffect;
        _pauseHandler.OnPauseDeactive += DisableMutedEffect;

        PlayMusic();
    }

    private void ApplyMutedEffect()
    {
        _audioFilter.cutoffFrequency = _lowPassValue;
    }

    private void DisableMutedEffect()
    {
        DOTween.To(() => _audioFilter.cutoffFrequency, value => _audioFilter.cutoffFrequency = value, _defaultValue, 1f)
            .SetEase(Ease.Linear);
    }

    public void PlayMusic()
    {
        PlayerSettings settings = PlayerData.Settings;
        _soundtrack.volume = settings.BaseVolume * settings.MusicVolume;
        _soundtrack.Play();
    }

    public void StopMusic()
    {
        _soundtrack.Stop();
    }
}
