using Core.Puzzle.Parameters;
using UnityEngine;

namespace Core.Puzzle
{
    [CreateAssetMenu(fileName = "PuzzleHouseData", menuName = "Data/Puzzle House Data", order = 1)]
    public class PuzzleHouseData : ScriptableObject
    {
        public string id;
        
        [Space]
        public DrinkParameter drink;
        public PetParameter pet;
    }
}