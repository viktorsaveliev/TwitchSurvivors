using UnityEngine;
using Zenject;

public class BitsInstaller : MonoInstaller
{
    [SerializeField] private BitsController _bitsController;

    public override void InstallBindings()
    {
        Container.BindInstance(_bitsController).AsSingle().NonLazy();
    }
}