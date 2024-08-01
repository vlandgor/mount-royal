using System;
using Core.Puzzle;
using Core.UI.Puzzle;
using Core.UI.Puzzle.PuzzlePanels;
using UnityEngine;

namespace Core.UI.Screens
{
    public class PuzzleScreenUI : ScreenUI
    {
        [SerializeField] private PuzzleHouseParametersPanel houseParametersPanel;
        [SerializeField] private PuzzleParameterOptionsPanel parameterOptionsPanel;
        [SerializeField] private PuzzleHintPanel puzzleHintPanel;

        private string activeHouseId;

        private void Start()
        {
            houseParametersPanel.OnParameterOptionPressed += HandleParameterPressed;
            houseParametersPanel.OnBackButtonPressed += HandleParameterBackButtonPressed;
            puzzleHintPanel.OnBackButtonPressed += HandleHintBackButtonPressed;
            
            parameterOptionsPanel.OnOptionSelected += HandleOptionSelected;
            parameterOptionsPanel.OnBackButtonPressed += HandleOptionBackButtonPressed;
        }

        private void OnDestroy()
        {
            houseParametersPanel.OnParameterOptionPressed -= HandleParameterPressed;
            houseParametersPanel.OnBackButtonPressed -= HandleParameterBackButtonPressed;
            puzzleHintPanel.OnBackButtonPressed -= HandleHintBackButtonPressed;
            
            parameterOptionsPanel.OnOptionSelected -= HandleOptionSelected;
            parameterOptionsPanel.OnBackButtonPressed -= HandleOptionBackButtonPressed;
        }

        public void ShowHint(string hint)
        {
            HidePanels();
            
            puzzleHintPanel.ShowHint(hint);
            puzzleHintPanel.gameObject.SetActive(true);
        }
        
        public void ShowHouseParameters(string houseId)
        {
            HidePanels();
            
            activeHouseId = houseId;
            
            HouseParameters parameters = PuzzleManager.Instance.GetHouseParameters(houseId);

            if (parameters != null)
            {
                houseParametersPanel.ShowParameters(parameters);
            }
            
            houseParametersPanel.gameObject.SetActive(true);
        }

        private void HandleParameterPressed(Type parameter)
        {
            HidePanels();
            
            parameterOptionsPanel.ShowOptions(parameter);
            
            parameterOptionsPanel.gameObject.SetActive(true);
        }
        
        private void HandleOptionSelected(Enum option)
        {
            HidePanels();
            
            PuzzleManager.Instance.UpdateProgress(activeHouseId, option);
            ShowHouseParameters(activeHouseId);
            
            houseParametersPanel.gameObject.SetActive(true);
        }
        
        private void HandleParameterBackButtonPressed()
        {
            HidePanels();
            
            UIManager.Instance.ShowGameScreen();
        }
        
        private void HandleOptionBackButtonPressed()
        {
            if (activeHouseId == null)
            {
                return;
            }
            
            ShowHouseParameters(activeHouseId);
        }
        
        private void HandleHintBackButtonPressed()
        {
            HidePanels();
            
            UIManager.Instance.ShowGameScreen();
        }

        private void HidePanels()
        {
            parameterOptionsPanel.gameObject.SetActive(false);
            houseParametersPanel.gameObject.SetActive(false);
            puzzleHintPanel.gameObject.SetActive(false);
        }
    }
}