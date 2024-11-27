using System;
using UnityEngine;

namespace Game.Scripts.UI.CoinAnimation
{
    public interface ICoinAnimationService
    {
        event Action<int> CoinEmitted;
        event Action<int> CoinArrived;
        void AnimateCoin(Vector3 fromPosition, int amount, float duration = 1.0f);
    }
}