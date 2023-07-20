using UnityEngine;
using Zenject;

public class ItemsInstaller : MonoInstaller
{
    [SerializeField] private ItemFactory _itemFactory;

    public override void InstallBindings()
    {
        Container.BindInstance(_itemFactory).AsSingle();
    }
}
