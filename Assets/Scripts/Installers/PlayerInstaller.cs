using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerUnit _playerUnit;

    public override void InstallBindings()
    {
        Container.BindInstance(_playerUnit).AsSingle().NonLazy();
    }
}