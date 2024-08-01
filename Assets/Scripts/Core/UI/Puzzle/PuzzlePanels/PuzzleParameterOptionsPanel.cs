using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Puzzle.PuzzlePanels
{
    public class PuzzleParameterOptionsPanel : PuzzlePanel
    {
        public event Action<Enum> OnOptionSelected;

        [SerializeField] private TMP_Text parameterName;
        [SerializeField] private OptionItemVisual optionItemVisualPrefab;
        
        [Space]
        [SerializeField] private Transform optionsPanelParent;

        private List<OptionItemVisual> optionsItemsVisual = new();

        
        
        public void ShowOptions(Type enumParameter)
        {
            ClearOptions();
            
            if (enumParameter.IsEnum)
            {
                var enumValues = Enum.GetValues(enumParameter);
                foreach (var value in enumValues)
                {
                    OptionItemVisual optionItemVisual = Instantiate(optionItemVisualPrefab, optionsPanelParent);
                    optionItemVisual.SetItem((Enum)value);
                    optionItemVisual.OnOptionSelected += HandleOptionSelected;
                    optionsItemsVisual.Add(optionItemVisual);
                }
            }
        }

        public void ClearOptions()
        {
            if (optionsItemsVisual == null)
                return;
            
            foreach (OptionItemVisual optionItem in optionsItemsVisual)
            {
                optionItem.OnOptionSelected -= HandleOptionSelected;
                Destroy(optionItem.gameObject);
            }
            optionsItemsVisual.Clear();
        }

        private void HandleOptionSelected(Enum option)
        {
            OnOptionSelected?.Invoke(option);
        }
    }
}
