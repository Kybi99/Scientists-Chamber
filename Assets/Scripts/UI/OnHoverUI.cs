using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
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
                tMPro.transform.position = Input.mousePosition + new Vector3(150, -10, 0);                                                                                                                   //check the active scene and on first scene update next frame for objects, if doors are open on click load next scene
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            canvasGroup.alpha = 1;
            CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.Portal);
            if (tMPro != null)
                tMPro.text = "Skladiste";

        }
        public void OnPointerExit(PointerEventData eventData)
        {
            canvasGroup.alpha = 0;
            CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.Arrow);
            if (tMPro != null)
                tMPro.text = "";

        }
    }
}


