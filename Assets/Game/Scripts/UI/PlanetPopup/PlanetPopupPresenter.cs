using System;
using Modules.Money;
using Modules.Planets;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI.PlanetPopup
{
    public class PlanetPopupPresenter : IPlanetPopupPresenter, IInitializable, IDisposable
    {
        public ReadOnlyReactiveProperty<Sprite> Icon => icon;
        public ReadOnlyReactiveProperty<string> Title => title;
        public ReadOnlyReactiveProperty<string> Population => population;
        public ReadOnlyReactiveProperty<string> Level => level;
        public ReadOnlyReactiveProperty<string> Income => income;
        public ReadOnlyReactiveProperty<string> UpgradePrice => upgradePrice;
        public ReadOnlyReactiveProperty<string> UpgradeButtonText => upgradeButtonText;
        public ReadOnlyReactiveProperty<bool> UpgradeButtonActive => upgradeButtonActive;
        public ReadOnlyReactiveProperty<bool> PricePanelActive => pricePanelActive;

        private readonly ReactiveProperty<Sprite> icon = new();
        private readonly ReactiveProperty<string> title = new("");
        private readonly ReactiveProperty<string> population = new("");
        private readonly ReactiveProperty<string> level = new("");
        private readonly ReactiveProperty<string> income = new("");
        private readonly ReactiveProperty<string> upgradePrice = new("");
        private readonly ReactiveProperty<string> upgradeButtonText = new("");
        private readonly ReactiveProperty<bool> upgradeButtonActive = new();
        private readonly ReactiveProperty<bool> pricePanelActive = new();


        private readonly IMoneyStorage moneyStorage;

        private IPlanet planet;

        public PlanetPopupPresenter(IMoneyStorage moneyStorage)
        {
            this.moneyStorage = moneyStorage;
        }

        public void Initialize()
        {
            moneyStorage.OnMoneyChanged += OnMoneyChanged;
        }

        public void Dispose()
        {
            if (planet != null)
            {
                planet.OnUpgraded -= OnPlanetUpgraded;
            }

            moneyStorage.OnMoneyChanged -= OnMoneyChanged;
        }

        public void UnlockOrUpgrade()
        {
            if (planet.CanUnlockOrUpgrade)
            {
                planet.UnlockOrUpgrade();
            }
        }

        public void SetPlanet(IPlanet planet)
        {
            if (this.planet != null)
            {
                this.planet.OnUpgraded -= OnPlanetUpgraded;
            }

            this.planet = planet;
            this.planet.OnUpgraded += OnPlanetUpgraded;
            this.planet.OnUnlocked += OnPlanetUnlocked;
            UpdateInfo();
        }

        private void OnPlanetUnlocked() => UpdateInfo();
        private void OnMoneyChanged(int newvalue, int prevvalue) => UpdateInfo();
        private void OnPlanetUpgraded(int newLevel) => UpdateInfo();

        private void UpdateInfo()
        {
            var unlocked = planet?.IsUnlocked ?? false;
            var maxLevelReached = planet?.IsMaxLevel ?? false;
            var canUnlockOrUpgrade = planet?.CanUnlockOrUpgrade ?? false;
            var price =  planet?.Price ?? 0;

            title.Value = planet?.Name ?? "";
            icon.Value = planet?.GetIcon(planet.IsUnlocked);
            population.Value = $"Population: {planet?.Population ?? 0}";
            level.Value = $"Level: {planet?.Level ?? 0}/{planet?.MaxLevel ?? 0}";
            income.Value = $"Income: {planet?.MinuteIncome ?? 0 / 60} / sec";
            upgradePrice.Value = price.ToString();
            upgradeButtonText.Value = !unlocked ? "Unlock" : maxLevelReached ? "MAX LEVEL" : "Upgrade";
            upgradeButtonActive.Value = canUnlockOrUpgrade;
            pricePanelActive.Value = !maxLevelReached;
        }
    }
}