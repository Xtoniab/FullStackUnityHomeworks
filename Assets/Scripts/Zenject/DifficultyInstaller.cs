using Modules;
using UnityEngine;

namespace Zenject
{
    [CreateAssetMenu(fileName = "DifficultyInstaller", menuName = "Installers/DifficultyInstaller")]
    public class DifficultyInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private int maxDifficulty = 9;

        public override void InstallBindings()
        {
            Container.Bind<IDifficulty>().To<Difficulty>().AsSingle().WithArguments(maxDifficulty);
            Container.BindInterfacesTo<DifficultyChangeObserver>().AsCached();
        }
    }
}