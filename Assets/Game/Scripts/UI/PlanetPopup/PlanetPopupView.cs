using System;
using Game.Scripts.UI.Common;
using Game.Scripts.Utils;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.UI.PlanetPopup
{
    public class PlanetPopupView: MonoBehaviour
    {
        [Header("Head")]
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Button closeButton;
        
        [Header("Content")]
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text populationText;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text incomeText;
        
        [Header("Upgrade")]
        [SerializeField] private Button upgradeButton;
        [SerializeField] private TextElement upgradePriceTextElement;
        [SerializeField] private TMP_Text upgradeButtonText;
        
        private IPlanetPopupPresenter presenter;
        private IDisposable disposable;

        [Inject]
        public void Construct(IPlanetPopupPresenter presenter)
        {
            this.presenter = presenter;
        }
        
        public void Show()
        {
            var d = Disposable.CreateBuilder();
            presenter.Icon.SubscribeAndCall(x => iconImage.sprite = x).AddTo(ref d);
            presenter.Title.SubscribeAndCall(x => titleText.text = x).AddTo(ref d);
            presenter.Population.SubscribeAndCall(x => populationText.text = x).AddTo(ref d);
            presenter.Level.SubscribeAndCall(x => levelText.text = x).AddTo(ref d);
            presenter.Income.SubscribeAndCall(x => incomeText.text = x).AddTo(ref d);
            presenter.UpgradePrice.SubscribeAndCall(x => upgradePriceTextElement.Text = x).AddTo(ref d);
            presenter.UpgradeButtonText.SubscribeAndCall(x => upgradeButtonText.text = x).AddTo(ref d);
            presenter.PricePanelActive.SubscribeAndCall(x => upgradePriceTextElement.gameObject.SetActive(x)).AddTo(ref d);
            presenter.UpgradeButtonActive.SubscribeAndCall(x => upgradeButton.interactable = x).AddTo(ref d);
            
            disposable = d.Build();

            closeButton.onClick.AddListener(Hide);
            upgradeButton.onClick.AddListener(presenter.UnlockOrUpgrade);
            
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            disposable?.Dispose();
            closeButton.onClick.RemoveAllListeners();
            upgradeButton.onClick.RemoveAllListeners();
            
            gameObject.SetActive(false);
        }
        
        private void OnDestroy()
        {
            disposable?.Dispose();
        }
    }
}