using TMPro;
using UnityEngine;

namespace Core.UI.Puzzle.PuzzlePanels
{
    public class PuzzleHintPanel : PuzzlePanel
    {
        [SerializeField] private TMP_Text hintText;

        public void ShowHint(string hint)
        {
            hintText.text = hint;
        }
    }
}