using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Money
{
    public class MoneyView : MonoBehaviour, IMoneyView
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text countText;

        public Vector3 IconPosition => icon.transform.position;
        
        private Tween currentTween;

        public void SetCountText(string count)
        {
            countText.text = count;
        }

        public void UpdateCountWithAnimation(int oldCount, int newCount)
        {
            currentTween?.Kill();

            var displayedValue = oldCount;
            
            currentTween = DOTween.To(
                () => displayedValue,
                x =>
                {
                    displayedValue = x;
                    countText.text = displayedValue.ToString();
                },
                newCount,
                1.0f
            ).SetEase(Ease.OutCubic).OnKill(() =>
            {
                countText.text = newCount.ToString();
            });
        }
    }
}