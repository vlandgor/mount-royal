using System;
using System.Collections.Generic;
using Core.Irritants;
using Core.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Character
{
    public class Character : MonoBehaviour
    {
        public event Action OnImmersionLost;
        
        private float irritantWeight;
        private List<IrritantBlocker> irritantBlockers = new();

        public float Immersion { get; private set; } = 100;
        public bool IsPlaying { get; private set; } = true;
        
        
        private readonly float minImmersion = 0;
        private readonly float maxImmersion = 100;

        private void Start()
        {
            ProcessImmersion().Forget();
        }

        public void ChangeIrritantWeight(IrritantType irritantType, float weight)
        {
            if(CheckIrritantForBlocker(irritantType))
                return;

            UIManager.Instance.ApplyIrritantEffectChange(weight > 0, irritantType);
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

        private bool CheckIrritantForBlocker(IrritantType irritantType)
        {
            foreach (IrritantBlocker blocker in irritantBlockers)
            {
                if (blocker.IrritantType == irritantType)
                    return true;
            }

            return false;
        }
    }
}