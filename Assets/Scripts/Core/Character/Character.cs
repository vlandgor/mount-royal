using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Character
{
    public class Character : MonoBehaviour
    {
        public event Action OnImmersionLost;
        
        private float irritantWeight;

        public float Immersion { get; private set; } = 100;
        public bool IsPlaying { get; private set; } = true;
        
        
        private readonly float minImmersion = 0;
        private readonly float maxImmersion = 100;

        private void Start()
        {
            ProcessImmersion().Forget();
        }

        public void ChangeIrritantWeight(float weight)
        {
            irritantWeight += weight;
        }

        private async UniTaskVoid ProcessImmersion()
        {
            while (IsPlaying)
            {
                if (Immersion <= minImmersion)
                {
                    IsPlaying = false;
                    OnImmersionLost?.Invoke();
                }

                Immersion -= irritantWeight * Time.deltaTime;
                
                await UniTask.Yield();
            }
        }
    }
}