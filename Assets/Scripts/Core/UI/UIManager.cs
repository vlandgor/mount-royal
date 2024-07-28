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

        private async void Start()
        {
            
        }

        public void ShowHousePuzzleScreen(string houseId)
        {
            puzzleScreenUI.ShowHouseParameters(houseId);
        }
    }
}
