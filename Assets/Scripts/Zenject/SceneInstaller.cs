using Modules;
using SnakeGame;
using UnityEngine;

namespace Zenject
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private WorldBounds worldBounds;

        public override void InstallBindings()
        {
            Container.Bind<IWorldBounds>().FromInstance(worldBounds).AsSingle();
            Container.Bind<IScore>().To<Score>().AsSingle();
            Container.BindInterfacesTo<GameStartObserver>().AsCached();
            Container.BindInterfacesAndSelfTo<GameCycle>().AsSingle();
        }
    }
}