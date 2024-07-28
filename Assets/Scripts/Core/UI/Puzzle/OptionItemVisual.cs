using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Puzzle
{
    public class OptionItemVisual : MonoBehaviour
    {
        public event Action<string> OnOptionSelected;
        
        [SerializeField] private TMP_Text parameterText;
        [SerializeField] private TMP_Text optionText;
        
        [SerializeField] private Button optionButton;

        private string parameterName;

        private void Start()
        {
            optionButton.onClick.AddListener(HandleOptionButtonPressed);
        }

        private void OnDestroy()
        {
            optionButton.onClick.RemoveListener(HandleOptionButtonPressed);
        }

        public void SetItem(string parameterName, string optionName)
        {
            this.parameterName = parameterName;
            
            parameterText.text = parameterName;
            optionText.text = optionName;
        }

        private void HandleOptionButtonPressed()
        {
            OnOptionSelected?.Invoke(parameterName);
        }
    }
}