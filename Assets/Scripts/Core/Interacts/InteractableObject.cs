using UnityEngine;

namespace Core.Interacts
{
    public abstract class InteractableObject : MonoBehaviour
    {
        [SerializeField] protected InteractableCollider interactableCollider;
        
        private void Start()
        {
            interactableCollider.OnInteractionFulfilled += HandleInteractionFullFilled;
        }

        private void OnDestroy()
        {
            interactableCollider.OnInteractionFulfilled += HandleInteractionFullFilled;
        }
        
        protected virtual void HandleInteractionFullFilled()
        {
            
        }
    }
}