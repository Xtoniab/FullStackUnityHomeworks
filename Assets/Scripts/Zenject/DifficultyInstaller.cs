using Modules;
using UnityEngine;

namespace Zenject
{
    public class DifficultyInstaller : Installer<int, DifficultyInstaller>
    {
        [Inject] private int maxDifficulty;

        public override void InstallBindings()
        {
            Container.Bind<IDifficulty>().To<Difficulty>().AsSingle().WithArguments(maxDifficulty);
            Container.BindInterfacesTo<DifficultyChangeObserver>().AsCached();
        }
    }
}