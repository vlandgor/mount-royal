using UnityEngine;

namespace Core.Irritants
{
    public class IrritableObject : MonoBehaviour
    {
        [SerializeField] private float weight;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Character.Character character))
            {
                character.ChangeIrritantWeight(weight);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Character.Character character))
            {
                character.ChangeIrritantWeight(-weight);
            }
        }
    }
}