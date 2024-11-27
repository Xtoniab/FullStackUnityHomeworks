using Modules.Planets;

namespace Game.Scripts.UI.PlanetPopup
{
    public class PlanetPopupShower: IPlanetPopupShower
    {
        private readonly PlanetPopupView view;
        private readonly PlanetPopupPresenter presenter;
        
        public PlanetPopupShower(PlanetPopupView view, PlanetPopupPresenter presenter)
        {
            this.view = view;
            this.presenter = presenter;
        }
        
        public void Show(IPlanet planet)
        {
            presenter.SetPlanet(planet);
            view.Show();
        }
    }
}