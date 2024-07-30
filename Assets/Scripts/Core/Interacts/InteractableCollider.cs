using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Core.Interacts
{
    public class InteractableCollider : MonoBehaviour
    {
        public event Action OnInteractionFulfilled;
        
        [SerializeField] private Collider interactableCollider;
        [SerializeField] private Image progressBar;
        
        [Header("Settings")]
        [SerializeField] private float requiredTime = 2f;
        [SerializeField] private float unfillSpeed = 1f;

        private CancellationTokenSource cancellationTokenSource;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Character.Character character))
            {
                StartInteractionProcess(character).Forget();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Character.Character character))
            {
                if (cancellationTokenSource != null)
                {
                    cancellationTokenSource.Cancel();
                    cancellationTokenSource.Dispose();
                    cancellationTokenSource = null;
                }
                
                UnfillProgressBar().Forget();
            }
        }

        private async UniTaskVoid StartInteractionProcess(Character.Character character)
        {
            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            float elapsedTime = 0f;

            while (elapsedTime < requiredTime)
            {
                elapsedTime += Time.deltaTime;
                if (progressBar != null)
                {
                    progressBar.fillAmount = elapsedTime / requiredTime;
                }
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }

            OnInteractionFulfilled?.Invoke();

            if (progressBar != null)
            {
                progressBar.fillAmount = 1f;
            }
        }

        private async UniTaskVoid UnfillProgressBar()
        {
            if (progressBar == null) return;

            while (progressBar.fillAmount > 0)
            {
                progressBar.fillAmount -= Time.deltaTime * unfillSpeed;
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            progressBar.fillAmount = 0f;
        }
    }
}