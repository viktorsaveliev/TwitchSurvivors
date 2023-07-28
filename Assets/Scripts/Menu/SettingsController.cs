using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [Header("Toggle buttons")]
    [SerializeField] private SettingsToggleButton _fullscreenMode;
    [SerializeField] private SettingsToggleButton _showDamage;
    [SerializeField] private SettingsToggleButton _screenShaking;
    [SerializeField] private SettingsToggleButton _twitchIntegration;

    [Header("Volume")]
    [SerializeField] private SettingsSlider _baseVolume;
    [SerializeField] private SettingsSlider _musicVolume;
    [SerializeField] private SettingsSlider _soundVolume;

    private void Awake()
    {
        InitToggleButtons();
        InitScrollbars();
    }

    private void OnEnable()
    {
        _fullscreenMode.OnClick += ChangeScreenMode;
        _showDamage.OnClick += ChangeDamageShowing;
        _screenShaking.OnClick += ChangeScreenShakeShowing;
        _twitchIntegration.OnClick += ChangeTwitchSetting;

        _baseVolume.OnValidate += SetBaseVolume;
        _musicVolume.OnValidate += SetMusicVolume;
        _soundVolume.OnValidate += SetSoundVolume;
    }

    private void OnDisable()
    {
        _fullscreenMode.OnClick -= ChangeScreenMode;
        _showDamage.OnClick -= ChangeDamageShowing;
        _screenShaking.OnClick -= ChangeScreenShakeShowing;
        _twitchIntegration.OnClick -= ChangeTwitchSetting;

        _baseVolume.OnValidate -= SetBaseVolume;
        _musicVolume.OnValidate -= SetMusicVolume;
        _soundVolume.OnValidate -= SetSoundVolume;
    }

    private void InitToggleButtons()
    {
        _fullscreenMode.Init();
        _showDamage.Init();
        _screenShaking.Init();
        _twitchIntegration.Init();

        _fullscreenMode.SetActive(Screen.fullScreen);
        _showDamage.SetActive(PlayerData.Settings.ShowDamage);
        _screenShaking.SetActive(PlayerData.Settings.ScreenShaking);
        _twitchIntegration.SetActive(PlayerData.Settings.TwitchIntegration);
    }

    private void InitScrollbars()
    {
        _baseVolume.Init();
        _musicVolume.Init();
        _soundVolume.Init();

        _baseVolume.SetValue(PlayerData.Settings.BaseVolume);
        _musicVolume.SetValue(PlayerData.Settings.MusicVolume);
        _soundVolume.SetValue(PlayerData.Settings.SoundVolume);
    }

    private void ChangeScreenMode(bool fullscreen) 
        => Screen.fullScreen = fullscreen;

    private void ChangeDamageShowing(bool enable) 
        => PlayerData.Settings.SetShowDamageSetting(enable);

    private void ChangeScreenShakeShowing(bool enable) 
        => PlayerData.Settings.SetScreenShakingSetting(enable);

    private void ChangeTwitchSetting(bool enable)
        => PlayerData.Settings.SetTwitchSetting(enable);

    private void SetBaseVolume(float value)
    {
        PlayerData.Settings.SetBaseVolume(value);
    }

    private void SetMusicVolume(float value)
    {
        PlayerData.Settings.SetMusicVolume(value);
    }

    private void SetSoundVolume(float value) 
        => PlayerData.Settings.SetSoundVolume(value);
}
