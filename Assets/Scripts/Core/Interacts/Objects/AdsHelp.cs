using UnityEngine;

namespace Core.Interacts.Objects
{
    public class AdsHelp : InteractableObject
    {
        protected override void HandleInteractionFullFilled()
        {
            Debug.Log("No internet connection");
        }
    }
}