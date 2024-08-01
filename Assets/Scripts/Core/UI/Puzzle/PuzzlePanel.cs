using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Puzzle
{
    public abstract class PuzzlePanel : MonoBehaviour
    {
        public event Action OnBackButtonPressed;
        
        [SerializeField] private Button backButton;
        
        private void Start()
        {
            backButton.onClick.AddListener(HandleBackButtonPressed);
        }

        private void OnDestroy()
        {
            backButton.onClick.RemoveListener(HandleBackButtonPressed);
        }
        
        private void HandleBackButtonPressed()
        {
            OnBackButtonPressed?.Invoke();
        }
    }
}