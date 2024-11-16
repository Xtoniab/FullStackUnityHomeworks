using Coins;
using Modules;
using UnityEngine;

namespace Zenject
{
    [CreateAssetMenu(fileName = "CoinSpawnerInstaller", menuName = "Installers/CoinSpawnerInstaller")]
    public class CoinSpawnerInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private Coin coinPrefab;

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