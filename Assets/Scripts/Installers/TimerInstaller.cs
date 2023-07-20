using UnityEngine;
using Zenject;

public class TimerInstaller : MonoInstaller
{
    [SerializeField] private Timer _timer;

    public override void InstallBindings()
    {
        Container.BindInstance(_timer).AsSingle();
    }
}