using Core.Puzzle;
using UnityEditor;
using UnityEngine;

namespace Editor.Riddle
{
    public class PuzzleProgressResetWindow : EditorWindow
    {
        [MenuItem("Vlandgor/Puzzle progress reset")]
        private static void ResetProgress()
        {
            PuzzleProgressSaver.ResetProgress();
            Debug.Log("Progress was reset");
        }
    }
}