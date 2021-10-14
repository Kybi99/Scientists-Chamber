using UnityEngine;
using UnityEngine.SceneManagement;

namespace FourGear.UI
{
    public class EndScreen : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        public Canvas portalCanvas;
        public static float alphaCount;
        private void Start()
        {
            alphaCount = 0;
            canvasGroup = this.GetComponentInChildren<CanvasGroup>();
            canvasGroup.interactable = false;
        }
        private void Update()
        {
            if(alphaCount < 300)
                alphaCount += canvasGroup.alpha;
            if (alphaCount >= 100)
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
