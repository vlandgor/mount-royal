using UnityEngine;

namespace Core.Puzzle
{
    [CreateAssetMenu(fileName = "PuzzleHouseData", menuName = "Data/Puzzle House Data", order = 1)]
    public class PuzzleHouseData : ScriptableObject
    {
        public string id;
        
        public string owner;
        public string drink;
        public string pet;
    }
}