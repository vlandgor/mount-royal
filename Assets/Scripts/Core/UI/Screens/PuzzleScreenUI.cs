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
            houseParametersPanel.OnParameterOptionPressed += HandleParameterPressed;
            parameterOptionsPanel.OnOptionSelected += HandleOptionSelected;
        }

        private void OnDestroy()
        {
            houseParametersPanel.OnParameterOptionPressed -= HandleParameterPressed;
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

        private void HandleParameterPressed(Type parameter)
        {
            parameterOptionsPanel.ShowOptions(parameter);
            
            houseParametersPanel.gameObject.SetActive(false);
            parameterOptionsPanel.gameObject.SetActive(true);
        }
        
        private void HandleOptionSelected(Enum option)
        {
            PuzzleManager.Instance.UpdateProgress(activeHouseId, option);
            ShowHouseParameters(activeHouseId);
            
            parameterOptionsPanel.gameObject.SetActive(false);
            houseParametersPanel.gameObject.SetActive(true);
        }
    }
}