using Core.Irritants;
using UnityEngine;

namespace Core.UI.Irritants
{
    public class IrritantEffect : MonoBehaviour
    {
        [SerializeField] private IrritantType irritantType;
        public IrritantType IrritantType => irritantType;
    }
}