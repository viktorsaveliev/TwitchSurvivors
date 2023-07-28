using UnityEngine;
using Zenject;

public class SettingsInstaller : MonoInstaller
{
    [SerializeField] private SettingsController _settings;

    public override void InstallBindings()
    {
        Container.BindInstance(_settings).AsSingle();
    }
}
