using System;
using Core.Puzzle;
using Core.UI.Puzzle;
using UnityEngine;

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
            houseParametersPanel.OnBackButtonPressed += HandleParameterBackButtonPressed;
            
            parameterOptionsPanel.OnOptionSelected += HandleOptionSelected;
            parameterOptionsPanel.OnBackButtonPressed += HandleOptionBackButtonPressed;
        }

        private void OnDestroy()
        {
            houseParametersPanel.OnParameterOptionPressed -= HandleParameterPressed;
            houseParametersPanel.OnBackButtonPressed -= HandleParameterBackButtonPressed;
            
            parameterOptionsPanel.OnOptionSelected -= HandleOptionSelected;
            parameterOptionsPanel.OnBackButtonPressed -= HandleOptionBackButtonPressed;
        }

        public void ShowHouseParameters(string houseId)
        {
            activeHouseId = houseId;
            
            HouseParameters parameters = PuzzleManager.Instance.GetHouseParameters(houseId);

            if (parameters != null)
            {
                houseParametersPanel.ShowParameters(parameters);
            }
            
            parameterOptionsPanel.gameObject.SetActive(false);
            houseParametersPanel.gameObject.SetActive(true);
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
        
        private void HandleParameterBackButtonPressed()
        {
            UIManager.Instance.ShowGameScreen();
        }
        
        private void HandleOptionBackButtonPressed()
        {
            if (activeHouseId == null)
            {
                return;
            }
            Debug.Log(activeHouseId);
            
            ShowHouseParameters(activeHouseId);
        }
    }
}