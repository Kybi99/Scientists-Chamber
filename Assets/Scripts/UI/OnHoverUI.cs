namespace FourGear
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    namespace FourGear
    {
        public class OnHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
        {
            CanvasGroup canvasGroup;
            private void Start()
            {
                canvasGroup = this.GetComponent<CanvasGroup>();
            }
            public void OnPointerEnter(PointerEventData eventData)
            {
                canvasGroup.alpha = 1;
                CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.Portal);
            }
            public void OnPointerExit(PointerEventData eventData)
            {
                canvasGroup.alpha = 0;
                CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.Arrow);

            }
        }
    }


}
