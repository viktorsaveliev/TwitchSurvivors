using UnityEngine;
using Zenject;

public class FXInstaller : MonoInstaller
{
    [SerializeField] private DeathFXController _deathFX;

    public override void InstallBindings()
    {
        Container.BindInstance(_deathFX).AsSingle();
    }
}
