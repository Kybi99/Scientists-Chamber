using UnityEngine;
using UnityEngine.UI;
using FourGear.Mechanics;
namespace FourGear.UI
{
    public class ShowHint : MonoBehaviour
    {
        private int numberOfClosedHints = 0;
        public static CanvasGroup canvasGroup;
        public GameObject helpScript;
        public static bool canClick;
        public static bool canZoom;
        public static bool canShowHint;
        public Animator animator;
        public static bool isFirstTimeInScene = true;
        private void Awake()
        {
            canvasGroup = GetComponentInChildren<CanvasGroup>();

            canShowHint = true;
            if (isFirstTimeInScene)
                canvasGroup.alpha = 1;
            else
            {
                animator.SetBool("OpenTheScroll", false);
                animator.SetBool("CloseTheScroll", true);
                canClick = true;
                canZoom = true;
            }
            if (ShowHint.isFirstTimeInScene)
                this.GetComponent<Canvas>().enabled = true;
            else
                this.GetComponent<Canvas>().enabled = false;
        }
        private void Update()
        {
            /*if (PauseMenu.gameIsPaused)
            {
                animator.SetBool("CloseTheScroll", true);
                this.GetComponent<Canvas>().enabled = false;
            }*/
        }
        public void ShowHints()
        {
            if (canShowHint && canvasGroup.alpha == 0)
            {
                animator.SetBool("OpenTheScroll", true);
                animator.SetBool("CloseTheScroll", false);

                this.GetComponent<Canvas>().enabled = true;

                canClick = false;
                canZoom = false;
                OnMouseEvents.numberOfMissedClicks--;
            }
            else if (!isFirstTimeInScene && canShowHint && canvasGroup.alpha == 0)
            {
                canvasGroup.alpha = 0;
                OnMouseEvents.numberOfMissedClicks--;
                animator.SetBool("OpenTheScroll", true);
                animator.SetBool("CloseTheScroll", false);

                this.GetComponent<Canvas>().enabled = true;

            }
            else if (canvasGroup.alpha == 1)
            {
                animator.SetBool("CloseTheScroll", true);
                animator.SetBool("OpenTheScroll", false);

                canClick = true;
                canZoom = true;
                OnMouseEvents.numberOfMissedClicks--;
            }

        }
        public void CloseHint()
        {
            OnMouseEvents.numberOfMissedClicks--;
            animator.SetBool("CloseTheScroll", true);
            animator.SetBool("OpenTheScroll", false);
            canClick = true;
            canZoom = true;
        }
    }

}