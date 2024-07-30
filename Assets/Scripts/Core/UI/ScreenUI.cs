using UnityEngine;

namespace Core.UI
{
    public class ScreenUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        public void SetEnabled(bool enable)
        {
            gameObject.SetActive(enable);
        }
    }
}
