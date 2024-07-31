using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class ImmersionBarUI : MonoBehaviour
    {
        [SerializeField] private Character.Character character;
        [SerializeField] private Slider immersionSlider;

        private void Update()
        {
            immersionSlider.value = character.Immersion / 100;
        }
    }
}
