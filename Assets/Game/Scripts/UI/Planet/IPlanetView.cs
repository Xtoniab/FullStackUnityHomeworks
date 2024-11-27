using System;
using UnityEngine;

namespace Game.Scripts.UI.Planet
{
    public interface IPlanetView
    {
        event Action OnClicked;
        event Action OnHeld;
        Vector3 CoinPosition { get; }

        void SetPlanetIcon(Sprite icon);
        void SetLockIconActive(bool active);
        void SetCoinIconActive(bool active);
        void SetUnlockPriceText(string text);
        void SetIncomeProgress(float value);
        void SetTimeToIncomeText(string text);
        void SetProgressBarVisible(bool visible);
        void SetUnlockPriceActive(bool active);
    }
}