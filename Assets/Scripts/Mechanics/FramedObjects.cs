using UnityEngine;
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
        private float rememberTime;
        private GameObject secondFrame;
        private NextScene nextScene;
        private PolygonCollider2D polygonCollider2D;
        private Animator animator1;
        private Animator animator2;
        private TMP_Text tMPro;
        private bool isMouseOnObject;
        private SpriteRenderer firstObjectRenderer;
        private SpriteRenderer secondObjectRenderer;
        public static bool isHighLightAllowed;
        public static bool isObjectMoved;
        public static Light2D doorLight;


        void Start()
        {
            //cursorMode = CursorMode.ForceSoftware;
            //Cursor.SetCursor(resetCursorTexture, Vector2.zero, cursorMode);
            isHighLightAllowed = true;
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
            string last9Letters = OnMouseEvents.CheckLast9LettersOfSceneName();

            if (Input.GetKeyDown(KeyCode.Mouse0) && isMouseOnObject && last9Letters == OnMouseEvents.sceneName && ShowHint.canClick)
            {
                FindValues();

                OnMouseEvents.numberOfMissedClicks--;
                firstObjectRenderer.enabled = false;

                if (secondObjectRenderer.enabled && (secondFrame.gameObject.name == "DoorsOpen" || secondFrame.gameObject.name == "DoorsOpenX") && ObjectPath.coroutineAllowed)
                {
                    LoadNextSceneIfDoorIsOpen();
                }
                else if ((this.gameObject.name == "DoorsClosed" || this.gameObject.name == "DoorsClosedX") && !secondObjectRenderer.enabled)
                {
                    OpenTheDoor();
                    isHighLightAllowed = false;
                }
                else
                {
                    EnableSecondFrame();
                }
            }
        }

        private void EnableSecondFrame()
        {
            secondObjectRenderer.enabled = true;
            if (polygonCollider2D != null)
                polygonCollider2D.enabled = false;
        }

        private void OpenTheDoor()
        {
            //Open the door if its closed 
            if (animator1 && animator2 != null)
            {
                rememberTime = TimerManager.timeValue;
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

        private void LoadNextSceneIfDoorIsOpen()
        {
            //Load next scene if door is open and none of the objects are moving
            if (rememberTime - TimerManager.timeValue >= 0.3f)
                nextScene.LoadNextScene();
            if (tMPro != null)
                tMPro.text = "";

            isMouseOnObject = false;
            isObjectMoved = true;
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
                if (secondObjectRenderer.enabled && tMPro != null && !PauseMenu.gameIsPaused)
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
            if (tMPro != null && PauseMenu.gameIsPaused || tMPro != null)
                tMPro.text = "";

            isMouseOnObject = false;

            CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.Arrow);
        }
    }
}