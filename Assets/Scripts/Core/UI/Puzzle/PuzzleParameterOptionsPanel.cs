using System;
using System.Collections.Generic;
using Core.Puzzle;
using Core.Riddle;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.UI.Puzzle
{
    public class PuzzleParameterOptionsPanel : MonoBehaviour
    {
        public event Action<string> OnOptionSelected;

        [Space] 
        [SerializeField] private TMP_Text parameterName;
        [SerializeField] private OptionItemVisual optionItemVisualPrefab;
        [SerializeField] private Button backButton;
        
        [Space]
        [SerializeField] private Transform optionsPanelParent;

        private List<ParameterItemVisual> optionsItemsVisual = new();

        public void ShowOptions()
        {
            
        }

        public void ClearOptions()
        {
            foreach (var optionItem in optionsItemsVisual)
            {
                optionItem.OnOptionSelected -= HandleOptionSelected;
                Destroy(optionItem.gameObject);
            }
            optionsItemsVisual.Clear();
        }

        private void HandleOptionSelected(string optionName)
        {
            OnOptionSelected?.Invoke(optionName);
        }
    }
}
