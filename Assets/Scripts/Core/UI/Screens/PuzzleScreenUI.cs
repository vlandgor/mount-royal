using System;
using System.Collections.Generic;
using Core.Puzzle;
using Core.Puzzle.Parameters;
using Core.Riddle;
using Core.UI.Puzzle;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.UI.Screens
{
    public class PuzzleScreenUI : ScreenUI
    {
        [SerializeField] private PuzzleHouseParametersPanel houseParametersPanel;
        [SerializeField] private PuzzleParameterOptionsPanel parameterOptionsPanel;

        private string activeHouseId;

        private void Start()
        {
            houseParametersPanel.OnParameterOptionPressed += HandleParameterOptionPressed;
            parameterOptionsPanel.OnOptionSelected += HandleOptionSelected;
        }

        private void OnDestroy()
        {
            houseParametersPanel.OnParameterOptionPressed -= HandleParameterOptionPressed;
            parameterOptionsPanel.OnOptionSelected -= HandleOptionSelected;
        }

        public void ShowHouseParameters(string houseId)
        {
            activeHouseId = houseId;
            
            HouseParameters parameters = PuzzleManager.Instance.GetHouseParameters(houseId);

            if (parameters != null)
            {
                houseParametersPanel.ShowParameters(parameters);
            }
        }

        private void HandleParameterOptionPressed(string parameterName)
        {
            parameterOptionsPanel.ShowOptions();
            
            houseParametersPanel.gameObject.SetActive(false);
            parameterOptionsPanel.gameObject.SetActive(true);
        }
        
        private void HandleOptionSelected(string optionName)
        {
            //PuzzleManager.Instance.UpdateProgress(activeHouseId, optionValue);
            
            parameterOptionsPanel.gameObject.SetActive(false);
            houseParametersPanel.gameObject.SetActive(true);
        }
    }
}