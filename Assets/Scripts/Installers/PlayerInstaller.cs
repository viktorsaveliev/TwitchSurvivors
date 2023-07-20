using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerUnit _playerUnit;
    [SerializeField] private PlayerInterface _playerInterface;

    public override void InstallBindings()
    {
        Container.BindInstance(_playerUnit).AsSingle();
        Container.BindInstance(_playerInterface).AsSingle();
    }
}