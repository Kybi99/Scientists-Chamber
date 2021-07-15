using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FourGear.UI;
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

        //private SpriteRenderer spriteRenderer;

        // Start is called before the first frame update
        void Start()
        {
            hotSpot = Vector2.zero;
            isMouseOnObject = false;
            nextScene = this.gameObject.GetComponent<NextScene>();
            secondFrame = this.transform.GetChild(0).gameObject;
            clickCount = 0;
            //isObjectMoved = false;
        }

        // Update is called once per frame
        void Update()
        {   
            if(Input.GetKeyDown(KeyCode.Mouse0) && isMouseOnObject)
            {
                FindValues();

                firstObjectRenderer.enabled = false;
            
                if (secondObjectRenderer.enabled && secondFrame.gameObject.name == "DoorsOpen" && ObjectPath.coroutineAllowed)
                {
                    for(int i = 0 ; i < firstFrameObjects.Length; i++)
                        firstFrameObjects[i].gameObject.SetActive(false);
                    //Debug.Log(secondObjectRenderer.enabled);\
                    nextScene.LoadNextScene();
                    isMouseOnObject = false;
                    Cursor.SetCursor(null, Vector2.zero, cursorMode);
                    isObjectMoved = true;
                }
                else
                    secondObjectRenderer.enabled = true;
            }
        }

        private void FindValues()
        {
            if (clickCount == 0)
            {
                firstFrameObjects = GameObject.FindGameObjectsWithTag("firstFrame");
                /*for(int i = 0 ; i < firstFrameObjects.Length; i++)
                    Debug.Log(firstFrameObjects[i]);*/
                secondFrame.GetComponent<SpriteRenderer>();
                firstObjectRenderer = this.gameObject.GetComponent<SpriteRenderer>();
                secondObjectRenderer = secondFrame.GetComponent<SpriteRenderer>();
                clickCount++;
                //Debug.Log(firstObjectRenderer);
                //Debug.Log(secondObjectRenderer);
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

        /*public static void DontChangeObjectState()
        {
            if(isObjectMoved)
            {
                firstObjectRenderer.enabled = false;
                secondObjectRenderer.enabled = true;
                //PreviousScene.isComingHome = false;
            }
        }*/
    }
}
