using UnityEngine;

namespace Core.Irritants
{
    public class IrritableObject : MonoBehaviour
    {
        [SerializeField] private IrritantType irritantType;
        
        [Space]
        [SerializeField] private float weight;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Character.Character character))
            {
                character.ChangeIrritantWeight(irritantType, weight);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Character.Character character))
            {
                character.ChangeIrritantWeight(irritantType, -weight);
            }
        }
    }
}