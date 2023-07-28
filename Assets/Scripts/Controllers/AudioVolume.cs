using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioVolume : MonoBehaviour
{
    public enum SoundType
    {
        Music,
        FX
    }

    [SerializeField] private SoundType _soundType;

    private AudioSource _soundtrack;

    private void Awake()
    {
        _soundtrack = GetComponent<AudioSource>();
        UpdateVolume();
    }

    private void OnEnable()
    {
        PlayerData.Settings.OnBaseVolumeChanged += UpdateVolume;

        if (_soundType == SoundType.Music)
        {
            PlayerData.Settings.OnMusicVolumeChanged += UpdateVolume;
        }
        else
        {
            PlayerData.Settings.OnSoundVolumeChanged += UpdateVolume;
        }
    }

    private void OnDisable()
    {
        PlayerData.Settings.OnBaseVolumeChanged -= UpdateVolume;

        if (_soundType == SoundType.Music)
        {
            PlayerData.Settings.OnMusicVolumeChanged -= UpdateVolume;
        }
        else
        {
            PlayerData.Settings.OnSoundVolumeChanged -= UpdateVolume;
        }
    }

    private void UpdateVolume()
    {
        PlayerSettings settings = PlayerData.Settings;

        float volume = _soundType == SoundType.Music ? settings.MusicVolume : settings.SoundVolume;
        _soundtrack.volume = settings.BaseVolume * volume;
    }
}
