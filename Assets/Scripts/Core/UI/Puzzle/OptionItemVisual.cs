using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Puzzle
{
    public class OptionItemVisual : MonoBehaviour
    {
        public event Action<Enum> OnOptionSelected;
        
        [SerializeField] private TMP_Text optionText;
        [SerializeField] private Button optionButton;

        private Enum option;

        private void Start()
        {
            optionButton.onClick.AddListener(HandleOptionButtonPressed);
        }
        private void OnDestroy()
        {
            optionButton.onClick.RemoveListener(HandleOptionButtonPressed);
        }

        public void SetItem(Enum option)
        {
            this.option = option;
            optionText.text = option.ToString();
        }

        private void HandleOptionButtonPressed()
        {
            OnOptionSelected?.Invoke(option);
        }
    }
}