using UnityEngine;
using UnityEngine.SceneManagement;

namespace FourGear.UI
{
    public class EndScreen : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        private void Start()
        {
            canvasGroup = this.GetComponent<CanvasGroup>();
            canvasGroup.interactable = false;
        }
        private void Update()
        {
            if(canvasGroup.alpha == 1)
                canvasGroup.interactable = true;
        }
        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
