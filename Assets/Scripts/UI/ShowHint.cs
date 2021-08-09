using UnityEngine;

namespace FourGear.UI
{
    public class ShowHint : MonoBehaviour
    {
        [SerializeField] private GameObject helpScript;
        public static bool canClick;
        public static bool canZoom;
        public static bool isFirstTimeInScene = true;
        private void Start()
        {
            if (isFirstTimeInScene)
                ShowHints();
            else
            {
                canClick = true;
                canZoom = true;
            }
        }
        public void ShowHints()
        {
            if (helpScript.activeSelf == false)
            {
                helpScript.SetActive(true);
                canClick = false;
                canZoom = false;
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