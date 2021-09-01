using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using FourGear.Mechanics;
namespace FourGear
{
    public class OnHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        CanvasGroup canvasGroup;
        private TMP_Text tMPro;

        private void Start()
        {
            canvasGroup = this.GetComponent<CanvasGroup>();
            tMPro = this.gameObject.GetComponentInChildren<TMP_Text>();
        }
        private void Update()
        {


            if (tMPro != null)
            {
                //Vector3 screenPoint = Input.mousePosition;
                //screenPoint.z = 10.0f;
                //tMPro.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
                //t//MPro.transform.position = Input.mousePosition + new Vector3(10, 1000, 0);                                                                                                                   //check the active scene and on first scene update next frame for objects, if doors are open on click load next scene

            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            canvasGroup.alpha = 1;
            WorkshopLight.workshopLight.enabled = false;
            CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.Portal);
            if (tMPro != null)
                tMPro.text = "Складиште";

        }
        public void OnPointerExit(PointerEventData eventData)
        {
            canvasGroup.alpha = 0;
            if (WorkshopLight.workshopLight != null)
                WorkshopLight.workshopLight.enabled = true;
            CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.Arrow);
            if (tMPro != null)
                tMPro.text = "";

        }
    }
}


