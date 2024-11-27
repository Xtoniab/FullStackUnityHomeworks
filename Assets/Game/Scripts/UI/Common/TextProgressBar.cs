using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Common
{
    public class TextProgressBar: MonoBehaviour
    {
        [SerializeField] private Image progressBarImage;
        [SerializeField] private TMP_Text text;
        
        public float Progress
        {
            get => progressBarImage.fillAmount;
            set => progressBarImage.fillAmount = value;
        }
        
        public string Text
        {
            get => text.text;
            set => text.text = value;
        }
    }
}