using UnityEngine;
using FourGear.Dialogue;

namespace FourGear.Mechanics
{
    public class DragAnDrop : MonoBehaviour
    {
        private DialogueTrigger dialogueTrigger;
        [HideInInspector] public GameObject correctForm;
        [HideInInspector] public Transform resetParent;
        [HideInInspector] public bool isMoving;
        [HideInInspector] public bool isFinished;
        [HideInInspector] public float startPosX;
        [HideInInspector] public float startPosY;
        [HideInInspector] public Vector3 resetPosition;
        [SerializeField] public bool thisObjectIsIn;
        public static int numberOfPartsIn = 0;

        private void Start()
        {
            thisObjectIsIn = false;
            resetPosition = this.transform.localPosition;
        }
        private void Update()
        {
            if (!isFinished)
            {
                if (isMoving)
                {
                    Vector3 mousePos;                                                                                                                                       
                    //get mouse position
                    mousePos = Input.mousePosition;
                    mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                    this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.localPosition.z);           
                    //change object position (follow the mouse)
                }
            }

        }

    }

}
