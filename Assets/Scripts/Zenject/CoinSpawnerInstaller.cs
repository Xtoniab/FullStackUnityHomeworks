using Coins;
using Modules;
using UnityEngine;

namespace Zenject
{
    public class CoinSpawnerInstaller : Installer<Coin, CoinSpawnerInstaller>
    {
        [Inject]
        private Coin coinPrefab;

        public override void InstallBindings()
        {
            Container.BindMemoryPool<Coin, CoinPool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(coinPrefab)
                .UnderTransformGroup("Coins");

            Container.Bind<ICoinSpawner>().To<CoinSpawner>().AsSingle();
        }
    }
}