using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;
using FourGear.UI;
using FourGear.Dialogue;


namespace FourGear.Mechanics
{
    public class FramedObjects : MonoBehaviour
    {
        [SerializeField] private CursorManager.CursorType cursorType;
        private int clickCount;
        private GameObject secondFrame;
        private NextScene nextScene;
        private PolygonCollider2D polygonCollider2D;
        private Animator animator1;
        private Animator animator2;
        private TMP_Text tMPro;
        private bool isMouseOnObject;
        public static bool isObjectMoved;
        private SpriteRenderer firstObjectRenderer;
        private SpriteRenderer secondObjectRenderer;
        public static Light2D doorLight;


        void Start()
        {
            //cursorMode = CursorMode.ForceSoftware;
            //Cursor.SetCursor(resetCursorTexture, Vector2.zero, cursorMode);
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
            if (Input.GetKeyDown(KeyCode.Mouse0) && isMouseOnObject && (SceneManager.GetActiveScene().name == "Skladiste" ||
                SceneManager.GetActiveScene().name == "PupinSkladiste" || SceneManager.GetActiveScene().name == "TeslaSkladiste") && ShowHint.canClick)
            {
                FindValues();

                OnMouseEvents.numberOfMissedClicks--;
                firstObjectRenderer.enabled = false;

                if (secondObjectRenderer.enabled && (secondFrame.gameObject.name == "DoorsOpen" || secondFrame.gameObject.name == "DoorsOpenX") && ObjectPath.coroutineAllowed)
                {
                    //Load next scene if door is open and none of the objects are moving

                    nextScene.LoadNextScene();
                    if (tMPro != null)
                        tMPro.text = "";

                    isMouseOnObject = false;
                    isObjectMoved = true;
                }
                else if ((this.gameObject.name == "DoorsClosed" || this.gameObject.name == "DoorsClosedX") && !secondObjectRenderer.enabled)
                {
                    //Open the door if they are closed 
                    if (animator1 && animator2 != null)
                    {
                        animator1.SetBool("isDoorReadyToOpen", true);
                        animator2.SetBool("isDoorReadyToOpen", true);

                        if (tMPro != null && doorLight != null)
                        {
                            tMPro.text = "Radna Soba";
                            CursorManager.Instance.SetActiveCursorType(cursorType);
                            doorLight.enabled = true;
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
            else if (Input.GetKeyDown(KeyCode.Mouse0) && isMouseOnObject && (SceneManager.GetActiveScene().name == "Radna soba" ||
                SceneManager.GetActiveScene().name == "PupinRadnaSoba" || SceneManager.GetActiveScene().name == "TeslaRadnaSoba"))
            {
                //load previous scene when click on door in radna soba scene

                if (!DialogueManager.isCorrectObjectIn)
                {
                    GetComponent<PreviousScene>().LoadPreviousScene();
                    isMouseOnObject = false;
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
                doorLight = secondObjectRenderer.GetComponentInChildren<Light2D>();

                clickCount++;
            }

        }

        void OnMouseEnter()
        {
            isMouseOnObject = true;

            if (secondObjectRenderer != null)
            {
                if (secondObjectRenderer.enabled && tMPro != null)
                {
                    CursorManager.Instance.SetActiveCursorType(cursorType);
                    tMPro.text = "Radna Soba";
                }
                else if (!secondObjectRenderer.enabled && tMPro != null)
                    CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.DoorFixed);
            }

            else if (nextScene != null && ShowHint.canClick)
                CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.DoorFixed);

            else if (ShowHint.canClick && nextScene == null)
            {
                //Cursor.SetCursor(resetCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
                CursorManager.Instance.SetActiveCursorType(cursorType);
            }
        }
        void OnMouseExit()
        {
            if (tMPro != null)
                tMPro.text = "";

            isMouseOnObject = false;

            CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.Arrow);
        }
    }
}