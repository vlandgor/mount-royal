using System;
using System.Collections.Generic;
using Core.Puzzle.Parameters;
using Core.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utilities;

namespace Core.Puzzle
{
    public class PuzzleManager : Singleton<PuzzleManager>
    {
        [SerializeField] private List<PuzzleHouseData> puzzleData = new();
        
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
                return parameters;
            }
            else
            {
                Debug.LogWarning($"No house parameters found for {houseId}");
                return null;
            }
        }

        public void UpdateProgress(string houseId, Enum value)
        {
            if (puzzleProgress.TryGetValue(houseId, out HouseParameters parameters))
            {
                switch (value)
                {
                    case DrinkParameter drink:
                        parameters.drink = drink;
                        break;
                    case PetParameter pet:
                        parameters.pet = pet;
                        break;
                }

                PuzzleProgressSaver.SavePuzzle(puzzleProgress);
            }
            else
            {
                Debug.LogWarning($"No house parameters found for {houseId}");
            }
        }

        public bool CheckPuzzleCorrectness()
        {
            foreach (var houseData in puzzleData)
            {
                if (puzzleProgress.TryGetValue(houseData.id, out HouseParameters parameters))
                {
                    if (parameters.drink != houseData.drink || parameters.pet != houseData.pet)
                    {
                        IsSolved = false;
                        return false;
                    }
                }
                else
                {
                    Debug.LogWarning($"No house parameters found for {houseData.id}");
                    IsSolved = false;
                    return false;
                }
            }
            
            IsSolved = true;
            return true;
        }
    }
}