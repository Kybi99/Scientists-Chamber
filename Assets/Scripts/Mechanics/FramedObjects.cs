using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FourGear.UI;
using FourGear.Dialogue;

namespace FourGear.Mechanics
{
    public class FramedObjects : MonoBehaviour
    {
        [SerializeField] private Texture2D resetCursorTexture;
        [SerializeField] private Texture2D cursorTexture;
        private CursorMode cursorMode;
        private Vector2 hotSpot;
        private GameObject secondFrame;
        public static GameObject[] firstFrameObjects;
        private NextScene nextScene;
        private bool isMouseOnObject;
        public static SpriteRenderer firstObjectRenderer;
        public static SpriteRenderer secondObjectRenderer;
        private PolygonCollider2D polygonCollider2D;

        void Start()
        {
            cursorMode = CursorMode.ForceSoftware;
            Cursor.SetCursor(resetCursorTexture, Vector2.zero, cursorMode);
            hotSpot = Vector2.zero;
            isMouseOnObject = false;
            nextScene = this.gameObject.GetComponent<NextScene>();
            if (this.transform.GetChild(0).gameObject != null)
                secondFrame = this.transform.GetChild(0).gameObject;

            polygonCollider2D = GetComponent<PolygonCollider2D>();
            firstFrameObjects = GameObject.FindGameObjectsWithTag("firstFrame");

        }
        void Update()
        {                                                                                                                                   //check the active scene and on first scene update next frame for objects, if doors are open on click load next scene
            FramedObjectClicked();
        }

        private void FramedObjectClicked()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && isMouseOnObject && SceneManager.GetActiveScene().name == "Skladiste")
            {
                FindValues();

                if (isMouseOnObject && ObjectPath.coroutineAllowed && (this.gameObject.name == "DoorsClosed" || this.gameObject.name == "DoorsClosedX"))           //Open the door
                {
                    nextScene.LoadNextScene();
                    isMouseOnObject = false;
                }
                else                                                                                                                                      //Show secdon frame and disable collider so object behind first frame can be clicked
                {
                    firstObjectRenderer.enabled = false;
                    secondObjectRenderer.enabled = true;
                    if (polygonCollider2D != null)
                        polygonCollider2D.enabled = false;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && isMouseOnObject && SceneManager.GetActiveScene().name == "Radna soba")                      //load previous scene when click on door in radna soba scene
            {
                if (!DialogueManager.isCorrectObjectIn)
                {
                    GetComponent<PreviousScene>().LoadPreviousScene();
                    isMouseOnObject = false;
                }
            }
        }

        private void FindValues()
        {
            firstObjectRenderer = this.gameObject.GetComponent<SpriteRenderer>();
            secondObjectRenderer = secondFrame.GetComponent<SpriteRenderer>();
        }

        void OnMouseEnter()
        {
            FindValues();

            if ((this.gameObject.name == "DoorsClosed" || this.gameObject.name == "DoorsClosedX"))                                                         //Show open doors on mouse hover
            {
                firstObjectRenderer.enabled = false;
                secondObjectRenderer.enabled = true;
            }

            isMouseOnObject = true;
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }

        void OnMouseExit()
        {

            if ((this.gameObject.name == "DoorsClosed" || this.gameObject.name == "DoorsClosedX"))
            {
                firstObjectRenderer.enabled = true;
                secondObjectRenderer.enabled = false;
            }

            isMouseOnObject = false;
            Cursor.SetCursor(resetCursorTexture, Vector2.zero, cursorMode);
        }
    }
}
