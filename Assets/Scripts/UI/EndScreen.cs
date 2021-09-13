using UnityEngine;
using UnityEngine.SceneManagement;

namespace FourGear.UI
{
    public class EndScreen : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        public Canvas portalCanvas;
        private void Start()
        {
            canvasGroup = this.GetComponentInChildren<CanvasGroup>();
            canvasGroup.interactable = false;
        }
        private void Update()
        {
            if (canvasGroup.alpha == 1)
            {
                canvasGroup.interactable = true;
                if (portalCanvas != null)
                    portalCanvas.enabled = false;
            }
            else
            {
                canvasGroup.interactable = false;
                if (portalCanvas != null)
                    portalCanvas.enabled = true;
            }


        }
        public void LoadMainMenu()
        {
            SceneManager.LoadScene(1);
        }
    }
}
