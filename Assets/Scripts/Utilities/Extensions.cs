using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Utilities
{
    public static class Extensions
    {
        public static T GetOrCreateComponent<T>(this GameObject gameObject) where T : Component
        {
            // Try to get the component
            T component = gameObject.GetComponent<T>();
        
            // If the component is not found, add it
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }
        
            return component;
        }
    }
}