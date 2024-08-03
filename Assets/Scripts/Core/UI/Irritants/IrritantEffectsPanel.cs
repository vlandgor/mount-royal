using System.Collections.Generic;
using Core.Irritants;
using UnityEngine;
using Utilities;
using Utilities.SerializableDictionary;

namespace Core.UI.Irritants
{
    public class IrritantEffectsPanel : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<IrritantType, IrritantEffect> irritantEffectsDictionary = new();

        private List<IrritantEffect> irritantEffects = new();

        public void AddEffect(IrritantType effectType)
        {
            if (irritantEffectsDictionary.TryGetValue(effectType, out IrritantEffect irritantEffect))
            {
                IrritantEffect effect = Instantiate(irritantEffect, transform);
                
                Debug.Log("Effect added");
                irritantEffects.Add(effect);
            }
        }

        public void RemoveEffect(IrritantType effectType)
        {
            IrritantEffect effectToRemove = irritantEffects.Find(effect => effect.IrritantType == effectType);
            
            if (effectToRemove != null)
            {
                Debug.Log("Effect removed");
                irritantEffects.Remove(effectToRemove);
                Destroy(effectToRemove.gameObject);
            }
        }
    }
}