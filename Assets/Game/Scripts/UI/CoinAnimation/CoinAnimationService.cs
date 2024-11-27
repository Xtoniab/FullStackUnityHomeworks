
using System;
using Game.Scripts.UI.CoinAnimation;
using Game.Scripts.UI.Money;
using Modules.Money;
using Modules.UI;
using UnityEngine;

public class CoinAnimationService : ICoinAnimationService
{
    public event Action<int> CoinEmitted;
    public event Action<int> CoinArrived;

    private readonly ParticleAnimator particleAnimator;
    private readonly Vector3 targetPosition;

    public CoinAnimationService(ParticleAnimator particleAnimator, Vector3 targetPosition)
    {
        this.particleAnimator = particleAnimator;
        this.targetPosition = targetPosition;
    }

    public void AnimateCoin(Vector3 fromPosition, int amount, float duration = 1.0f)
    {
        CoinEmitted?.Invoke(amount);

        particleAnimator.Emit(fromPosition, targetPosition, duration, () =>
        {
            CoinArrived?.Invoke(amount);
        });
    }
}