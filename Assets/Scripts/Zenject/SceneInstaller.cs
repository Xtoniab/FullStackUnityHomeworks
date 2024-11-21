using Modules;
using SnakeGame;
using UnityEngine;

namespace Zenject
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Snake snake;
        [SerializeField] private GameUI gameUI;
        [SerializeField] private WorldBounds worldBounds;

        public override void InstallBindings()
        {
            SnakeInstaller.Install(Container, snake);
            GameUIInstaller.Install(Container, gameUI);
            
            Container.Bind<IWorldBounds>().FromInstance(worldBounds).AsSingle();
            Container.Bind<IScore>().To<Score>().AsSingle();
            
            Container.BindInterfacesTo<GameStartObserver>().AsCached();
            Container.BindInterfacesAndSelfTo<GameCycle>().AsSingle();
        }
    }
}