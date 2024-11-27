using System;
using System.Text;
using Game.Scripts.UI.CoinAnimation;
using Game.Scripts.UI.Money;
using Game.Scripts.UI.PlanetPopup;
using Modules.Money;
using Modules.Planets;
using Modules.UI;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI.Planet
{
    public class PlanetPresenter : IInitializable, IDisposable
    {
        private readonly IPlanet planet;
        private readonly IPlanetView view;
        private readonly ICoinAnimationService coinAnimationService;
        private readonly IPlanetPopupShower planetPopupShower;

        public PlanetPresenter(
            IPlanet planet,
            IPlanetView view,
            ICoinAnimationService coinAnimationService,
            IPlanetPopupShower planetPopupShower)
        {
            this.planet = planet;
            this.view = view;
            this.coinAnimationService = coinAnimationService;
            this.planetPopupShower = planetPopupShower;
        }

        public void Initialize()
        {
            planet.OnIncomeTimeChanged += OnIncomeTimeChanged;
            planet.OnUnlocked += OnUnlocked;
            planet.OnIncomeReady += OnIncomeReady;

            view.OnClicked += OnPlanetClicked;
            view.OnHeld += OnPlanetHeld;

            SetInitialState();
        }

        private void SetInitialState()
        {
            SetUnlock(planet.IsUnlocked);
            OnIncomeReady(planet.IsIncomeReady);
            UpdateProgressBarVisible();
        }

        public void Dispose()
        {
            planet.OnIncomeTimeChanged -= OnIncomeTimeChanged;
            planet.OnUnlocked -= OnUnlocked;

            view.OnClicked -= OnPlanetClicked;
            view.OnHeld -= OnPlanetHeld;
        }

        private void UpdateProgressBarVisible()
        {
            view.SetProgressBarVisible(planet.IsUnlocked && !planet.IsIncomeReady);
        }

        private void SetUnlock(bool unlocked)
        {
            view.SetLockIconActive(!unlocked);
            view.SetUnlockPriceActive(!unlocked);
            view.SetUnlockPriceText(planet.Price.ToString());
            view.SetPlanetIcon(planet.GetIcon(unlocked));
            UpdateProgressBarVisible();
        }

        private void OnIncomeReady(bool isReady)
        {
            view.SetCoinIconActive(isReady);
            UpdateProgressBarVisible();
        }

        private void OnPlanetClicked()
        {
            if (planet.CanUnlock)
            {
                planet.Unlock();
                return;
            }

            if (planet.IsIncomeReady)
            {
                coinAnimationService.AnimateCoin(view.CoinPosition, planet.MinuteIncome);
                planet.GatherIncome();
                return;
            }
        }

        private void OnPlanetHeld()
        {
            planetPopupShower.Show(planet);
        }

        private void OnIncomeTimeChanged(float remainingTime)
        {
            var hours = (int)Mathf.Floor(remainingTime / (60 * 60));
            var minutes = (int)Mathf.Floor((remainingTime % (60 * 60)) / 60);
            var seconds = (int)Mathf.Ceil(remainingTime % 60);
            
            var sb = new StringBuilder();
    
            if (hours > 0) sb.Append($"{hours}h:");
            if (minutes > 0 || hours > 0) sb.Append($"{minutes}m:");
            sb.Append($"{seconds}s");

            view.SetTimeToIncomeText(sb.ToString());
            view.SetIncomeProgress(planet.IncomeProgress);
        }
        private void OnUnlocked()
        {
            SetUnlock(planet.IsUnlocked);
        }
    }
}