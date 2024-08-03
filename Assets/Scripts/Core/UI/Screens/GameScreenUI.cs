using Core.Irritants;
using Core.UI.Irritants;
using UnityEngine;

namespace Core.UI.Screens
{
    public class GameScreenUI : ScreenUI
    {
        [SerializeField] private IrritantEffectsPanel irritantEffectsPanel;

        public void ApplyEffectChange(bool affected, IrritantType irritantType)
        {
            if (affected)
            {
                irritantEffectsPanel.AddEffect(irritantType);
            }
            else
            {
                irritantEffectsPanel.RemoveEffect(irritantType);
            }
        }
    }
}
