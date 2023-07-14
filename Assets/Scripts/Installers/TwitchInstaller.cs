using UnityEngine;
using Zenject;

public class TwitchInstaller : MonoInstaller
{
    [SerializeField] private TwitchIntegration _twitchIntegration;

    public override void InstallBindings()
    {
        Container.BindInstance(_twitchIntegration).AsSingle().NonLazy();
    }
}