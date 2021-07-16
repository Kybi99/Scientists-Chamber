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

        void Start()
        {
            hotSpot = Vector2.zero;
            isMouseOnObject = false;
            nextScene = this.gameObject.GetComponent<NextScene>();
            if(this.transform.GetChild(0).gameObject != null)
                secondFrame = this.transform.GetChild(0).gameObject;
            clickCount = 0;
        }
        void Update()
        {                                                                                                                                   //check the active scene and on first scene update next frame for objects, if doors are open on click load next scene
            if(Input.GetKeyDown(KeyCode.Mouse0) && isMouseOnObject && SceneManager.GetActiveScene().name =="Skladiste")
            {
                FindValues();

                firstObjectRenderer.enabled = false;
            
                if (secondObjectRenderer.enabled && secondFrame.gameObject.name == "DoorsOpen" && ObjectPath.coroutineAllowed)
                {
                    for(int i = 0 ; i < firstFrameObjects.Length; i++)
                        firstFrameObjects[i].gameObject.SetActive(false);
                    nextScene.LoadNextScene();
                    isMouseOnObject = false;
                    Cursor.SetCursor(null, Vector2.zero, cursorMode);
                    isObjectMoved = true;
                }
                else
                    secondObjectRenderer.enabled = true;
            }
            else if(Input.GetKeyDown(KeyCode.Mouse0) && isMouseOnObject && SceneManager.GetActiveScene().name =="Radna soba")                      //load previous scene when click on door in radna soba scene
            {
                if(!DialogueManager.isCorrectObjectIn)
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
                secondFrame.GetComponent<SpriteRenderer>();
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
