using System;
using UnityEngine;

namespace Core.Irritants
{
    public class Irritant : MonoBehaviour
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