using System;
using Core.Puzzle;
using Core.UI;
using UnityEngine;

namespace Core.Interacts.Objects
{
    public class House : InteractableObject
    {
        [Header("House")] 
        [SerializeField] private PuzzleHouseData houseData;

        protected override void HandleInteractionFullFilled()
        {
            UIManager.Instance.ShowHousePuzzleScreen(houseData.id);
        }
    }
}