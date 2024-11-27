using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.Common
{
    public class TextElement: MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        
        public string Text
        {
            get => text.text;
            set => text.text = value;
        }
    }
}