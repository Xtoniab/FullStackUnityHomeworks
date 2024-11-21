using SnakeGame;
using UI;
using UnityEngine;

namespace Zenject
{
    public class GameUIInstaller: Installer<GameUI, GameUIInstaller>
    {
        [Inject] 
        private IGameUI gameUI;
        
        public override void InstallBindings()
        {
            Container.Bind<IGameUI>().FromInstance(gameUI).AsSingle();
            Container.BindInterfacesTo<GameUIPresenter>().AsCached();
        }
    }
}