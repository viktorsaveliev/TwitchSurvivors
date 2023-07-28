using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerUnit _playerUnit;
    [SerializeField] private ShopUI _shopUI;
    [SerializeField] private PlayerInputController _playerInput;

    public override void InstallBindings()
    {
        Container.BindInstance(_playerUnit).AsSingle();
        Container.BindInstance(_shopUI).AsSingle();
        Container.Bind<IInputControl>().FromInstance(_playerInput).AsSingle();
    }
}