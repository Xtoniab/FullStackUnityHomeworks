using R3;
using UnityEngine;

namespace Game.Scripts.UI.PlanetPopup
{
    public interface IPlanetPopupPresenter
    {
        ReadOnlyReactiveProperty<Sprite> Icon { get; }
        ReadOnlyReactiveProperty<string> Title { get; }
        ReadOnlyReactiveProperty<string> Population { get; }
        ReadOnlyReactiveProperty<string> Level { get; }
        ReadOnlyReactiveProperty<string> Income { get; }
        ReadOnlyReactiveProperty<string> UpgradePrice { get; }
        ReadOnlyReactiveProperty<string> UpgradeButtonText { get; }
        ReadOnlyReactiveProperty<bool> UpgradeButtonActive { get; }
        ReadOnlyReactiveProperty<bool> PricePanelActive { get; }
        

        void UnlockOrUpgrade();
    }
}