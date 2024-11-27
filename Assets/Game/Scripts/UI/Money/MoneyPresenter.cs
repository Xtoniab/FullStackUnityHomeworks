using System;
using Game.Scripts.UI.CoinAnimation;
using Modules.Money;
using Zenject;

namespace Game.Scripts.UI.Money
{
    public class MoneyPresenter : IInitializable, IDisposable
    {
        private readonly IMoneyStorage moneyStorage;
        private readonly IMoneyView view;
        private readonly ICoinAnimationService coinAnimationService;

        private int displayedMoney;
        private int delayedAmount;

        public MoneyPresenter(IMoneyStorage moneyStorage,
            IMoneyView view,
            ICoinAnimationService coinAnimationService)
        {
            this.moneyStorage = moneyStorage;
            this.view = view;
            this.coinAnimationService = coinAnimationService;
        }

        public void Initialize()
        {
            moneyStorage.OnMoneyChanged += OnMoneyChanged;
            coinAnimationService.CoinEmitted += OnCoinEmitted;
            coinAnimationService.CoinArrived += OnCoinArrived;
            view.SetCountText(moneyStorage.Money.ToString());
            
            displayedMoney = moneyStorage.Money;
        }

        public void Dispose()
        {
            moneyStorage.OnMoneyChanged -= OnMoneyChanged;
            coinAnimationService.CoinEmitted -= OnCoinEmitted;
            coinAnimationService.CoinArrived -= OnCoinArrived;
        }

        private void OnCoinEmitted(int count)
        {
            delayedAmount += count;
        }

        private void OnCoinArrived(int count)
        {
            var prevDisplayedMoney = displayedMoney;
            displayedMoney += count;
            delayedAmount -= count;
            UpdateMoneyView(prevDisplayedMoney, displayedMoney);
        }

        private void OnMoneyChanged(int newValue, int prevValue)
        {
            var prevDisplayedMoney = displayedMoney;
            displayedMoney = newValue - delayedAmount;
            UpdateMoneyView(prevDisplayedMoney, displayedMoney);
        }
        
        private void UpdateMoneyView(int fromValue, int toValue)
        {
            if (toValue > fromValue)
            {
                view.UpdateCountWithAnimation(fromValue, toValue);
            }
            else
            {
                view.SetCountText(toValue.ToString());
            }
        }
    }
}