using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using FourGear.UI;
using FourGear.Dialogue;


namespace FourGear.Mechanics
{
    public class FramedObjects : MonoBehaviour
    {
        [SerializeField] private Texture2D resetCursorTexture;
        [SerializeField] private Texture2D cursorTexture;
        private CursorMode cursorMode;
        [SerializeField] private Vector2 hotSpot;
        private GameObject secondFrame;
        private NextScene nextScene;
        private bool isMouseOnObject;
        public static bool isObjectMoved;
        private int clickCount;
        public static SpriteRenderer firstObjectRenderer;
        public static SpriteRenderer secondObjectRenderer;
        private PolygonCollider2D polygonCollider2D;
        private Animator animator1;
        private Animator animator2;
        private TMP_Text tMPro;


        void Start()
        {
            cursorMode = CursorMode.ForceSoftware;
            Cursor.SetCursor(resetCursorTexture, Vector2.zero, cursorMode);
            //hotSpot = Vector2.zero;
            isMouseOnObject = false;
            nextScene = this.gameObject.GetComponent<NextScene>();
            if (this.transform.GetChild(0).gameObject != null)
                secondFrame = this.transform.GetChild(0).gameObject;
            clickCount = 0;
            polygonCollider2D = GetComponent<PolygonCollider2D>();

        }
        void Update()
        {
            if (tMPro != null)
                tMPro.transform.position = Input.mousePosition + new Vector3(-150, -10, 0);                                                                                                                   //check the active scene and on first scene update next frame for objects, if doors are open on click load next scene
            FramedObjectClicked();
        }

        private void FramedObjectClicked()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && isMouseOnObject && (SceneManager.GetActiveScene().name == "Skladiste" || SceneManager.GetActiveScene().name == "PupinSkladiste" || SceneManager.GetActiveScene().name == "TeslaSkladiste"))
            {
                FindValues();

                firstObjectRenderer.enabled = false;

                if (secondObjectRenderer.enabled && (secondFrame.gameObject.name == "DoorsOpen" || secondFrame.gameObject.name == "DoorsOpenX") && ObjectPath.coroutineAllowed)           //Open the door
                {
                    nextScene.LoadNextScene();
                    if (tMPro != null)
                        tMPro.text = "";
                    isMouseOnObject = false;
                    Cursor.SetCursor(resetCursorTexture, Vector2.zero, cursorMode);
                    isObjectMoved = true;
                }
                else if ((this.gameObject.name == "DoorsClosed" || this.gameObject.name == "DoorsClosedX") && !secondObjectRenderer.enabled)
                {
                    if (animator1 && animator2 != null)
                    {
                        animator1.SetBool("isDoorReadyToOpen", true);
                        animator2.SetBool("isDoorReadyToOpen", true);

                        if (tMPro != null)
                        {
                            tMPro.text = "Radna Soba";
                            //tMPro.transform.position = Input.mousePosition + new Vector3(-10, 0, 0);
                        }
                    }
                }
                else
                {
                    secondObjectRenderer.enabled = true;
                    if (polygonCollider2D != null)
                        polygonCollider2D.enabled = false;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && isMouseOnObject && (SceneManager.GetActiveScene().name == "Radna soba" || SceneManager.GetActiveScene().name == "PupinRadnaSoba" || SceneManager.GetActiveScene().name == "TeslaRadnaSoba"))                      //load previous scene when click on door in radna soba scene
            {
                if (!DialogueManager.isCorrectObjectIn)
                {
                    GetComponent<PreviousScene>().LoadPreviousScene();
                    isMouseOnObject = false;
                    Cursor.SetCursor(resetCursorTexture, Vector2.zero, cursorMode);
                }
            }
        }

        private void FindValues()
        {
            if (clickCount == 0)
            {
                firstObjectRenderer = this.gameObject.GetComponent<SpriteRenderer>();
                animator1 = GetComponent<Animator>();
                secondObjectRenderer = secondFrame.GetComponent<SpriteRenderer>();
                animator2 = secondObjectRenderer.GetComponent<Animator>();
                tMPro = secondObjectRenderer.GetComponentInChildren<TMP_Text>();
                clickCount++;
            }

        }

        void OnMouseEnter()
        {
            if (secondObjectRenderer != null)
                if (secondObjectRenderer.enabled && tMPro != null)
                {
                    tMPro.text = "Radna Soba";
                }

            isMouseOnObject = true;
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }

        void OnMouseExit()
        {
            if (tMPro != null)
            {
                tMPro.text = "";
            }

            isMouseOnObject = false;
            Cursor.SetCursor(resetCursorTexture, Vector2.zero, cursorMode);

        }
    }
}