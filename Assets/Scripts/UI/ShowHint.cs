using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourGear.UI
{
    public class ShowHint : MonoBehaviour
    {
        //[SerializeField] private GameObject Canvas;
        [SerializeField] private GameObject helpScript;
        public static bool canClick;
        public static bool canZoom;
        private void Start()
        {
            canClick = true;
            canZoom = true;
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