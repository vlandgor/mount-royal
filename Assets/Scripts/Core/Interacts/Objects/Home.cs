using Core.Puzzle;
using UnityEngine;

namespace Core.Interacts.Objects
{
    public class Home : InteractableObject
    {
        protected override void HandleInteractionFullFilled()
        {
            // TODO: Finish night
            if (PuzzleManager.Instance.CheckPuzzleCorrectness())
            {
                Debug.Log("Puzzle solved");
            }
            else
            {
                Debug.Log("Puzzle NOT solved");
            }
        }
    }
}