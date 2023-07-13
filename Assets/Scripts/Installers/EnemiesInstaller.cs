using UnityEngine;
using Zenject;

public class EnemiesInstaller : MonoInstaller
{
    [SerializeField] private EnemyFactory _enemyFactory;

    public override void InstallBindings()
    {
        Container.BindInstance(_enemyFactory).AsSingle();

        EnemyDetection enemyDetection = new(_enemyFactory.GetAllEnemies);
        Container.Bind<IEnemyDetection>().FromInstance(enemyDetection).AsSingle();
    }
}