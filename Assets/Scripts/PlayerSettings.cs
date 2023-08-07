using System;

public class PlayerSettings
{
    public event Action OnBaseVolumeChanged;
    public event Action OnMusicVolumeChanged;
    public event Action OnSoundVolumeChanged;

    public bool ShowDamage => _showDamage;
    public bool ScreenShaking => _screenShaking;
    public bool TwitchIntegration => _twitchIntegration;

    public float BaseVolume => _baseVolume;
    public float MusicVolume => _musicVolume;
    public float SoundVolume => _soundVolume;

    private bool _showDamage = true;
    private bool _screenShaking = true;
    private bool _twitchIntegration = true;

    private float _baseVolume = 1f;
    private float _musicVolume = 0.05f;
    private float _soundVolume = 0.3f;

    public void SetShowDamageSetting(bool enable)
    {
        _showDamage = enable;
    }

    public void SetScreenShakingSetting(bool enable)
    {
        _screenShaking = enable;
    }

    public void SetTwitchSetting(bool enable)
    {
        _twitchIntegration = enable;
    }

    public void SetBaseVolume(float volume)
    {
        if (volume < 0 || volume > 1) return;
        _baseVolume = volume;

        OnBaseVolumeChanged?.Invoke();
    }

    public void SetMusicVolume(float volume)
    {
        if (volume < 0 || volume > 1) return;
        _musicVolume = volume;

        OnMusicVolumeChanged?.Invoke();
    }

    public void SetSoundVolume(float volume)
    {
        if (volume < 0 || volume > 1) return;
        _soundVolume = volume;

        OnSoundVolumeChanged?.Invoke();
    }
}
