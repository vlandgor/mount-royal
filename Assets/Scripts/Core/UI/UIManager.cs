using System;
using Core.UI.Screens;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utilities;

namespace Core.UI
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private GameScreenUI gameScreenUI;
        [SerializeField] private PuzzleScreenUI puzzleScreenUI;

        private void Start()
        {
            ShowGameScreen();
        }

        public void ShowGameScreen()
        {
            HideScreens();

            gameScreenUI.SetEnabled(true);
        }

        public void ShowHousePuzzleScreen(string houseId)
        {
            HideScreens();
            
            puzzleScreenUI.SetEnabled(true);
            puzzleScreenUI.ShowHouseParameters(houseId);
        }

        public void ShowHintPuzzleScreen(string hint)
        {
            HideScreens();
            
            puzzleScreenUI.SetEnabled(true);
            puzzleScreenUI.ShowHint(hint);
        }

        private void HideScreens()
        {
            gameScreenUI.SetEnabled(false);
            puzzleScreenUI.SetEnabled(false);
        }
    }
}
