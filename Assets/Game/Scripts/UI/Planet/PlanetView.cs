using System;
using Game.Scripts.UI.Common;
using Modules.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Planet
{
    public class PlanetView: MonoBehaviour, IPlanetView
    {
        public event Action OnClicked; 
        public event Action OnHeld; 
        
        [SerializeField] private SmartButton selfButton;
        [SerializeField] private Image planetIcon;
        [SerializeField] private GameObject lockIcon;
        [SerializeField] private GameObject coinIcon;
        [SerializeField] private TextElement unlockPriceText;
        [SerializeField] private TextProgressBar incomeProgressBar;

        public Vector3 CoinPosition => coinIcon.transform.position;

        private void OnEnable()
        {
            selfButton.OnClick += OnClick;
            selfButton.OnHold += OnHold;
        }

        private void OnDisable()
        {
            selfButton.OnClick -= OnClick;
            selfButton.OnHold -= OnHold;
        }

        private void OnClick() => OnClicked?.Invoke();
        private void OnHold() => OnHeld?.Invoke();

        public void SetPlanetIcon(Sprite icon) => planetIcon.sprite = icon;
        public void SetLockIconActive(bool active) => lockIcon.SetActive(active);
        public void SetCoinIconActive(bool active) => coinIcon.SetActive(active);
        public void SetUnlockPriceText(string text) => unlockPriceText.Text = text;
        public void SetIncomeProgress(float value) => incomeProgressBar.Progress = value;
        public void SetTimeToIncomeText(string text) => incomeProgressBar.Text = text;
        public void SetUnlockPriceActive(bool active) => unlockPriceText.gameObject.SetActive(active);
        public void SetProgressBarVisible(bool visible) => incomeProgressBar.gameObject.SetActive(visible);
    }
}