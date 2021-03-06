using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;
using FourGear.UI;

namespace FourGear.Mechanics
{
    public class FramedObjects : MonoBehaviour
    {
        [SerializeField] private CursorManager.CursorType cursorType;
        private int clickCount;
        private int indexSceneCheck ;
        private float rememberTime;
        private GameObject secondFrame;
        private NextScene nextScene;
        private PolygonCollider2D polygonCollider2D;
        private Animator animatorClosedDoor;
        private Animator animatorOpenedDoor;
        private TMP_Text tMPro;
        private bool isMouseOnObject;
        private SpriteRenderer firstObjectRenderer;
        private SpriteRenderer secondObjectRenderer;
        public static bool isHighLightAllowed;
        public static bool isObjectMoved;
        public static Light2D doorLight;


        void Start()
        {
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
            if (Input.GetKeyDown(KeyCode.Mouse0) && isMouseOnObject && OnMouseEvents.CheckIfFirstSceneIsActive() && ShowHint.canClick)
            {
               // Debug.Log(ObjectMovement.numberOfObjectsFlying);
                FindValues();

                firstObjectRenderer.enabled = false;

                Debug.Log(ObjectMovement.isNextSceneAllowed);

                if (secondObjectRenderer.enabled && (secondFrame.gameObject.name == "DoorsOpen" || secondFrame.gameObject.name == "DoorsOpenX") && ObjectMovement.coroutineAllowed && ObjectMovement.isNextSceneAllowed)
                {
                    if (OnMouseEvents.numberOfMissedClicks % 10 == 9)
                        OnMouseEvents.numberOfMissedClicks++;
                    LoadNextSceneIfDoorIsOpen();
                }
                else if ((this.gameObject.name == "DoorsClosed" || this.gameObject.name == "DoorsClosedX") && !secondObjectRenderer.enabled)
                {

                    OpenTheDoor();
                    isHighLightAllowed = false;
                }
                else
                    EnableSecondFrame();
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
            if (animatorClosedDoor && animatorOpenedDoor != null)
            {
                rememberTime = TimerManager.timeValue;
                animatorClosedDoor.SetBool("isDoorReadyToOpen", true);
                animatorOpenedDoor.SetBool("isDoorReadyToOpen", true);

                if (tMPro != null && doorLight != null)
                {
                    tMPro.text = "?????????? ????????";
                    CursorManager.Instance.SetActiveCursorType(cursorType);
                    doorLight.enabled = true;
                }
            }
        }

        private void LoadNextSceneIfDoorIsOpen()
        {

            //Load next scene if door is open and none of the objects are moving
            if (rememberTime - TimerManager.timeValue >= 0.4f)
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
                animatorClosedDoor = GetComponent<Animator>();
                secondObjectRenderer = secondFrame.GetComponent<SpriteRenderer>();
                animatorOpenedDoor = secondObjectRenderer.GetComponent<Animator>();
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
                if (secondObjectRenderer.enabled && tMPro != null && !PauseMenu.gameIsPaused && ShowHint.canClick)
                {
                    CursorManager.Instance.SetActiveCursorType(cursorType);
                    tMPro.text = "?????????? ????????";
                }
                else if (!secondObjectRenderer.enabled && tMPro != null)
                    CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.DoorFixed);
            }

            else if (nextScene != null && ShowHint.canClick)
                CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.DoorFixed);

            else if (ShowHint.canClick && nextScene == null)
            {
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