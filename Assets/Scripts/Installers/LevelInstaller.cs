using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Notify _notify;

    public override void InstallBindings()
    {
        Container.BindInstance(_timer).AsSingle();
        Container.BindInstance(_notify).AsSingle();
    }
}