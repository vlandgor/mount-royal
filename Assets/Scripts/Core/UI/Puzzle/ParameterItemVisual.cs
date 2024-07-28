using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Puzzle
{
    public class ParameterItemVisual : MonoBehaviour
    {
        public event Action<Type> OnParameterSelected;
        
        [SerializeField] private TMP_Text parameterText;
        [SerializeField] private TMP_Text optionText;
        
        [SerializeField] private Button optionButton;

        private Type parameter;

        private void Start()
        {
            optionButton.onClick.AddListener(HandleOptionButtonPressed);
        }

        private void OnDestroy()
        {
            optionButton.onClick.RemoveListener(HandleOptionButtonPressed);
        }

        public void SetItem(Type parameter, Enum option)
        {
            this.parameter = parameter;
            
            string parameterName = parameter.Name;
            if (parameterName.EndsWith("Parameter"))
            {
                parameterName = parameterName.Substring(0, parameterName.Length - "Parameter".Length);
            }

            parameterText.text = parameterName;
            
            optionText.text = Enum.GetName(parameter, option);
        }

        private void HandleOptionButtonPressed()
        {
            OnParameterSelected?.Invoke(parameter);
        }
    }
}