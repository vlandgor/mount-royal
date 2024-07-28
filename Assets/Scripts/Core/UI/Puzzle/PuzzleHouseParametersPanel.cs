using System;
using System.Collections.Generic;
using System.Reflection;
using Core.Puzzle;
using Core.Puzzle.Parameters;
using Core.UI.Puzzle;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.UI
{
    public class PuzzleHouseParametersPanel : MonoBehaviour
    {
        public event Action<Type> OnParameterOptionPressed;

        [SerializeField] private TMP_Text houseName;
        [SerializeField] private ParameterItemVisual parameterItemVisualPrefab;
        [SerializeField] private Button backButton;
        
        [Space]
        [SerializeField] private Transform parametersPanelParent;

        private List<ParameterItemVisual> parameterItemsVisual = new();

        public void ShowParameters(HouseParameters houseParameters)
        {
            ClearParameters();

            Type houseParametersType = typeof(HouseParameters);
            FieldInfo[] fields = houseParametersType.GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    Enum enumValue = (Enum)field.GetValue(houseParameters);
                    ParameterItemVisual parameterItemVisual = Instantiate(parameterItemVisualPrefab, parametersPanelParent);
                    parameterItemVisual.SetItem(field.FieldType, enumValue);
                    parameterItemsVisual.Add(parameterItemVisual);
                }
                else
                {
                    Debug.Log($"{field.Name} is not an Enum");
                }
            }

            foreach (ParameterItemVisual parameterItem in parameterItemsVisual)
            {
                parameterItem.OnParameterSelected += HandleParameterSelected;
            }
        }

        public void ClearParameters()
        {
            if(parameterItemsVisual == null)
                return;
            
            foreach (ParameterItemVisual parameterItem in parameterItemsVisual)
            {
                parameterItem.OnParameterSelected -= HandleParameterSelected;
                Destroy(parameterItem.gameObject);
            }
            parameterItemsVisual.Clear();
        }

        private void HandleParameterSelected(Type parameter)
        {
            OnParameterOptionPressed?.Invoke(parameter);
        }
    }
}
