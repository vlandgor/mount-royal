using System;
using System.Collections.Generic;
using Core.Puzzle.Parameters;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utilities;

namespace Core.Puzzle
{
    public class PuzzleManager : Singleton<PuzzleManager>
    {
        public bool IsSolved { get; private set; }

        private Dictionary<string, HouseParameters> puzzleProgress = new();

        private async void Start()
        {
            await LoadPuzzleProgress();
        }

        private async UniTask LoadPuzzleProgress()
        {
            puzzleProgress = await PuzzleProgressSaver.LoadPuzzle();
        }

        public HouseParameters GetHouseParameters(string houseId)
        {
            if (puzzleProgress.TryGetValue(houseId, out HouseParameters parameters))
            {
                Debug.Log($"House parameters for {houseId}: {parameters.owner}, {parameters.drink}, {parameters.pet}");
                return parameters;
            }
            else
            {
                Debug.LogWarning($"No house parameters found for {houseId}");
                return null;
            }
        }

        public void UpdateProgress(string houseId, string parameter, string option)
        {
            
        }
    }
}