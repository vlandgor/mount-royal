using UnityEngine;

namespace Core.Irritants
{
    public class IrritantBlocker : MonoBehaviour
    {
        [SerializeField] private IrritantType irritantType;
        public IrritantType IrritantType => irritantType;
    }
}
