using Core.UI;
using UnityEngine;

namespace Core.Interacts.Objects
{
    public class Npc : InteractableObject
    {
        [Header("Hint")]
        [TextArea(3, 5)] 
        [SerializeField] private string hint;
        
        protected override void HandleInteractionFullFilled()
        {
            UIManager.Instance.ShowHintPuzzleScreen(hint);
        }
    }
}