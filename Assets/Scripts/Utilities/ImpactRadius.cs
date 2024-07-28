using System;
using UnityEngine;

namespace Utilities
{
    [RequireComponent(typeof(SphereCollider))]
    public class ImpactRadius : MonoBehaviour
    {
        [SerializeField] private float radius = 1.0f;
        [SerializeField] private Color gizmoColor = Color.yellow;

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}
