using UnityEngine;

namespace Game.Scripts.UI.Money
{
    public interface IMoneyView
    {
        void SetCountText(string count);
        void UpdateCountWithAnimation(int oldCount, int newCount);
    }
}