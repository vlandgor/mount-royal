using System;
using System.Collections.Generic;
using System.Reflection;
using Core.Puzzle;
using Core.UI.Puzzle;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.UI
{
    public class PuzzleHouseParametersPanel : MonoBehaviour
    {
        public event Action<string> OnParameterOptionPressed;

        [SerializeField] private TMP_Text houseName;
        [SerializeField] private ParameterItemVisual parameterItemVisualPrefab;
        [SerializeField] private Button backButton;
        
        [Space]
        [SerializeField] private Transform parametersPanelParent;

        private List<ParameterItemVisual> parameterItemsVisual = new();

        public void ShowParameters(HouseParameters houseParameters)
        {
            ParameterItemVisual parameterItemVisualOwner = Instantiate(parameterItemVisualPrefab, parametersPanelParent);
            parameterItemVisualOwner.SetItem("Owner", houseParameters.owner.ToString());
            parameterItemsVisual.Add(parameterItemVisualOwner);
            
            ParameterItemVisual parameterItemVisualDrink = Instantiate(parameterItemVisualPrefab, parametersPanelParent);
            parameterItemVisualDrink.SetItem("Drink", houseParameters.drink.ToString());
            parameterItemsVisual.Add(parameterItemVisualDrink);
            
            ParameterItemVisual parameterItemVisualPet = Instantiate(parameterItemVisualPrefab, parametersPanelParent);
            parameterItemVisualPet.SetItem("Pet", houseParameters.pet.ToString());
            parameterItemsVisual.Add(parameterItemVisualPet);

            foreach (ParameterItemVisual parameterItem in parameterItemsVisual)
            {
                parameterItem.OnOptionSelected += HandleOptionSelected;
            }
        }

        public void ClearParameters()
        {
            foreach (var parameterItem in parameterItemsVisual)
            {
                parameterItem.OnOptionSelected -= HandleOptionSelected;
                Destroy(parameterItem.gameObject);
            }
            parameterItemsVisual.Clear();
        }

        private void HandleOptionSelected(string parameterName)
        {
            OnParameterOptionPressed?.Invoke(parameterName);
        }
    }
}
