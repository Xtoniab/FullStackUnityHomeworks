using System;
using Game.Scripts.UI.CoinAnimation;
using Game.Scripts.UI.Money;
using Game.Scripts.UI.Planet;
using Game.Scripts.UI.PlanetPopup;
using Modules.Planets;
using Modules.UI;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private PlanetPopupView planetPopupView;
        [SerializeField] private MoneyView moneyView;
        [SerializeField] private PlanetView[] planetViews;
        [SerializeField] private ParticleAnimator coinParticleAnimator;

        public override void InstallBindings()
        {
            Container
                .Bind<ICoinAnimationService>()
                .To<CoinAnimationService>()
                .AsSingle()
                .WithArguments(coinParticleAnimator, moneyView.IconPosition);
            
            MoneyUIInstaller.Install(Container, moneyView);
            PlanetsUIInstaller.Install(Container, planetViews);
            PlanetPopupInstaller.Install(Container, planetPopupView);
        }
    }
}