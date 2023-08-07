using DG.Tweening;
using UnityEngine;

public class AudioController
{
    private readonly AudioSource _soundtrack;
    private readonly AudioLowPassFilter _audioFilter;
    private readonly PauseHandler _pauseHandler;
    private readonly AudioClip _baseMusic;
    private readonly AudioClip _skillzorRap;

    private readonly float _defaultValue = 10000f;
    private readonly float _lowPassValue = 600f;

    public AudioController(AudioSource audioSource, AudioLowPassFilter audioFilter, PauseHandler pauseHandler, AudioClip skillzorRap)
    {
        _soundtrack = audioSource;
        _audioFilter = audioFilter;
        _pauseHandler = pauseHandler;
        _skillzorRap = skillzorRap;
        
        _baseMusic = _soundtrack.clip;
    }

    public void Init()
    {
        _pauseHandler.OnPauseActive += ApplyMutedEffect;
        _pauseHandler.OnPauseDeactive += DisableMutedEffect;

        PlaySoundtrack();
    }

    public void PlaySoundtrack()
    {
        PlayerSettings settings = PlayerData.Settings;
        _soundtrack.clip = _baseMusic;
        _soundtrack.volume = settings.BaseVolume * settings.MusicVolume;
        _soundtrack.loop = true;
        _soundtrack.Play();
    }

    public void PlaySkillzorRap()
    {
        PlayerSettings settings = PlayerData.Settings;
        _soundtrack.clip = _skillzorRap;
        _soundtrack.volume = settings.BaseVolume * settings.MusicVolume;
        _soundtrack.Play();
    }

    public void StopMusic()
    {
        _soundtrack.Stop();
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
}
