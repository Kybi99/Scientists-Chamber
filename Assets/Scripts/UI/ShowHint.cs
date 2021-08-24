using UnityEngine;
using FourGear.Mechanics;

namespace FourGear.UI
{
    public class ShowHint : MonoBehaviour
    {
        [SerializeField] private GameObject helpScript;
        public static bool canClick;
        public static bool canZoom;
        public static bool canShowHint;

        public static bool isFirstTimeInScene = true;
        private void Start()
        {
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
            if (helpScript.activeSelf == false && canShowHint)
            {
                helpScript.SetActive(true);
                canClick = false;
                canZoom = false;
                OnMouseEvents.numberOfMissedClicks--;
            }
            else
            {
                helpScript.SetActive(false);
                canClick = true;
                canZoom = true;
            }

        }
        public void CloseHint()
        {
            helpScript.SetActive(false);
            canClick = true;
            canZoom = true;
        }
    }

}