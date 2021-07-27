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
        [SerializeField] private Texture2D cursorTexture;
        [SerializeField] private CursorMode cursorMode = CursorMode.Auto;
        private Vector2 hotSpot;
        private GameObject secondFrame;
        public static GameObject[] firstFrameObjects;
        private NextScene nextScene;
        private bool isMouseOnObject;
        public static bool isObjectMoved;
        private int clickCount;
        public static SpriteRenderer firstObjectRenderer;
        public static SpriteRenderer secondObjectRenderer;
        private PolygonCollider2D polygonCollider2D;

        void Start()
        {
            hotSpot = Vector2.zero;
            isMouseOnObject = false;
            nextScene = this.gameObject.GetComponent<NextScene>();
            if (this.transform.GetChild(0).gameObject != null)
                secondFrame = this.transform.GetChild(0).gameObject;
            clickCount = 0;
            polygonCollider2D = GetComponent<PolygonCollider2D>();
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

                firstObjectRenderer.enabled = false;

                if (secondObjectRenderer.enabled && (secondFrame.gameObject.name == "DoorsOpen" || secondFrame.gameObject.name == "DoorsOpenX") && ObjectPath.coroutineAllowed)           //Open the door
                {
                    nextScene.LoadNextScene();
                    isMouseOnObject = false;
                    Cursor.SetCursor(null, Vector2.zero, cursorMode);
                    isObjectMoved = true;
                }
                else if ((this.gameObject.name == "DoorsOpen" || this.gameObject.name == "DoorsOpenX") && !secondObjectRenderer.enabled)
                    secondObjectRenderer.enabled = true;
                else
                {
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
                    Cursor.SetCursor(null, Vector2.zero, cursorMode);
                }
            }
        }

        private void FindValues()
        {
            if (clickCount == 0)
            {
                firstFrameObjects = GameObject.FindGameObjectsWithTag("firstFrame");
                firstObjectRenderer = this.gameObject.GetComponent<SpriteRenderer>();
                secondObjectRenderer = secondFrame.GetComponent<SpriteRenderer>();
                clickCount++;
            }

        }

        void OnMouseEnter()
        {
            isMouseOnObject = true;
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }

        void OnMouseExit()
        {
            isMouseOnObject = false;
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}
