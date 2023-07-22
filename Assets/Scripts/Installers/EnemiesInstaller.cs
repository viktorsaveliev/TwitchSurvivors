using UnityEngine;
using Zenject;

public class EnemiesInstaller : MonoInstaller
{
    [SerializeField] private EnemyFactory _enemyFactory;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_enemyFactory).AsSingle();

        EnemyCounter enemyCounter = new(_enemyFactory);

        Container.Bind<IEnemyCounter>().FromInstance(enemyCounter).AsSingle();
        Container.Bind<EnemyCounter>().FromInstance(enemyCounter).AsSingle();
    }
}