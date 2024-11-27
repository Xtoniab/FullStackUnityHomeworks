using Modules.Planets;
using Zenject;

namespace Game.Scripts.UI.Planet
{
    public class PlanetsUIInstaller: Installer<PlanetView[], PlanetsUIInstaller>
    {
        [Inject]
        private readonly PlanetView[] planetViews;
        
        public override void InstallBindings()
        {
            var planets = Container.Resolve<IPlanet[]>();

            for (var i = 0; i < planets.Length; i++)
            {
                Container
                    .BindInterfacesAndSelfTo<PlanetPresenter>()
                    .AsCached()
                    .WithArguments(planets[i], planetViews[i])
                    .NonLazy();
            }
        }
    }
}