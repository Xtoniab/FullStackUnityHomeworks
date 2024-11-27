using Zenject;

namespace Game.Scripts.UI.PlanetPopup
{
    public class PlanetPopupInstaller: Installer<PlanetPopupView, PlanetPopupInstaller>
    {
        [Inject]
        private readonly PlanetPopupView planetPopupView;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<PlanetPopupPresenter>()
                .AsSingle();

            Container
                .Bind<PlanetPopupView>()
                .FromInstance(planetPopupView)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<PlanetPopupShower>()
                .AsSingle();
        }
    }
}