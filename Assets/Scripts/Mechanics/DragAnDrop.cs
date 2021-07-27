using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FourGear.UI;
using FourGear.Dialogue;

namespace FourGear.Mechanics
{
    public class DragAnDrop : MonoBehaviour
    {
        [HideInInspector] public GameObject correctForm;
        [HideInInspector] public Transform resetParent;
        [HideInInspector] public bool isMoving;
        [HideInInspector] public bool isFinished;
        [HideInInspector] public float startPosX;
        [HideInInspector] public float startPosY;
        public static int numberOfPartsIn = 0;
        [HideInInspector] public Vector3 resetPosition;
        //[SerializeField] private DialogueTrigger dialogueTrigger;
        private DialogueTrigger dialogueTrigger;
        [HideInInspector] public bool thisObjectIsIn;
        public static GameObject[] placeholders;

        private void Start()
        {
            placeholders = GameObject.FindGameObjectsWithTag("placeholders");
            thisObjectIsIn = false;
            //dialogueTrigger = FindObjectOfType<DialogueManager>().GetComponent<DialogueTrigger>();
            resetPosition = this.transform.localPosition;
        }
        private void Update()
        {
            if (!isFinished)
            {
                if (isMoving)
                {
                    Vector3 mousePos;                                                                                                                                       //get mouse position
                    mousePos = Input.mousePosition;
                    mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                    this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.localPosition.z);           //change object position (follow the mouse)
                }
            }

        }

    }

}
