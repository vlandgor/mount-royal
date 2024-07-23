using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.UI
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private UIDocument uiDocument;
        
        private ProgressBar progressBar;
        
        private void Start()
        {
            VisualElement rootElement = uiDocument.rootVisualElement;

            progressBar = rootElement.Q<ProgressBar>("ImmersionValue");

            progressBar.value = 100;
        }
    }
}