using Coins;
using Inputs;
using Modules;
using SnakeGame;
using UnityEngine;

namespace Zenject
{
    public class SnakeInstaller : MonoInstaller
    {
        [SerializeField] private Snake snake;

        public override void InstallBindings()
        {
            Container.Bind<ISnake>().FromInstance(snake).AsSingle();
            Container.Bind<ISnakeInput>().To<SnakeInput>().AsSingle();
            Container.BindInterfacesTo<SnakeController>().AsCached();
            Container.BindInterfacesTo<SnakeSelfCollideObserver>().AsCached();
            Container.BindInterfacesTo<SnakeOutOfBoundsObserver>().AsCached();
            Container.BindInterfacesTo<SnakeCoinCollectObserver>().AsCached();
        }
    }
}