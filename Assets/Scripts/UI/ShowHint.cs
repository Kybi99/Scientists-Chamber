using UnityEngine;
using UnityEngine.UI;
using FourGear.Mechanics;
namespace FourGear.UI
{
    public class ShowHint : MonoBehaviour
    {
        [SerializeField] private GameObject helpScript;
        private int numberOfClosedHints = 0;
        public Image image;
        private Color tempColor;
        public static bool canClick;
        public static bool canZoom;
        public static bool canShowHint;
        public Animator animator;
        public static bool isFirstTimeInScene = true;
        private void Start()
        {
            tempColor = image.color;

            canShowHint = true;
            if (isFirstTimeInScene)
                ShowHints();
            else
            {
                canClick = true;
                canZoom = true;
            }
        }
        private void Update()
        {
            if (PauseMenu.gameIsPaused)
                helpScript.SetActive(false);
        }
        public void ShowHints()
        {
            if (helpScript.GetComponent<Image>().color.a == 0 && canShowHint)
            {
                tempColor.a = 1f;
                image.color = tempColor;

                animator.SetBool("CloseTheScroll", false);
                canClick = false;
                canZoom = false;
                OnMouseEvents.numberOfMissedClicks--;
            }
            else
            {
                if (numberOfClosedHints == 0)
                {
                    animator.SetBool("CloseTheScroll", true);
                }
                /* else
                     helpScript.SetActive(false);*/
                animator.SetBool("CloseTheScroll", true);
                canClick = true;
                canZoom = true;
            }

        }
        public void CloseHint()
        {
            if (numberOfClosedHints == 0)
            {
                animator.SetBool("CloseTheScroll", true);
            }
            /*else
                helpScript.SetActive(false);*/
            canClick = true;
            canZoom = true;
        }
        public void DisableClick()
        {
            tempColor.a = 1f;
            image.color = tempColor;

            canClick = false;
            canZoom = false;
        }
    }

}