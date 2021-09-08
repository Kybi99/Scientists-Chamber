using UnityEngine;
using UnityEngine.SceneManagement;
using FourGear.Singletons;
using FourGear.UI;
using FourGear.Dialogue;
using TMPro;

namespace FourGear.Mechanics
{
    public class OnMouseEvents : MonoBehaviour
    {
        [SerializeField] private FindEmptySlot findEmptySlot;
        [SerializeField] private ObjectMovement objectMovement;
        [SerializeField] private DragAnDrop dragAnDrop;
        public static int numberOfMissedClicks = 0;
        private static int sceneIndex;
        private int sceneIndexCheck;
        private int index;
        private int rememberClicks;
        private int rememberLastTimeClicks;
        private float rememberTime;
        private TMP_Text tMPro;
        private string backgroundName;
        private Quaternion resetRotation;
        public static string sceneName;
        public static string sceneName2;
        public Particles particles;


        private void Start()
        {
            rememberLastTimeClicks = 0;
            rememberClicks = 0;
            backgroundName = "Pozadina";
        }
        private void Update()
        {
            if (tMPro != null)
                tMPro.transform.position = Input.mousePosition + new Vector3(-25, 0, 0);
            if (rememberTime - TimerManager.timeValue > 6 || TimerManager.timeValue == 0 && tMPro != null)
                tMPro.text = "";
        }
        //OnClickFunctions
        public void OnMouseDown()
        {
            //sceneIndexCheck = CheckIfFirstSceneIsActive();
            //sceneName = "Skladiste";
            if (Input.GetMouseButtonDown(0) && CheckIfFirstSceneIsActive() && ShowHint.canClick)
            {
                OnClick();
            }

            //DragAndDrop  
            else if (Input.GetMouseButtonDown(0) && !CheckIfFirstSceneIsActive() && ShowHint.canClick)
            {
                OnDrag();
            }
        }
        private void OnMouseUp()
        {
            //sceneIndexCheck = CheckIfFirstSceneIsActive();
            OnDrop();
        }
        private void OnClick()
        {
            Debug.Log(ObjectMovement.routeToGo);
            if (this.gameObject.name == backgroundName)
            {
                rememberClicks = numberOfMissedClicks;
                CalculateMissClicks();
            }
            else if (!objectMovement.inInventory && ObjectMovement.coroutineAllowed && ObjectMovement.routeToGo < Inventory.arraySlots.Length && !objectMovement.thisObjectIsFlying )
            {
                foreach (Transform child in transform)
                    Destroy(child.gameObject);

                numberOfMissedClicks--;
               // if (this.gameObject.transform.childCount == 0 )
                    particles.PlayParticle(this.gameObject.transform);
                ObjectMovement.routeToGo = findEmptySlot.CheckFirstEmptySlot(Inventory.arraySlots);
                Debug.Log(ObjectMovement.routeToGo);
                if (ObjectMovement.routeToGo != -1)
                    StartCoroutine(objectMovement.GoByTheRoute(ObjectMovement.routeToGo));
            }

            else if (objectMovement.inInventory && ObjectMovement.coroutineAllowed && !objectMovement.thisObjectIsFlying)
            {
                foreach (Transform child in transform)
                    Destroy(child.gameObject);

                numberOfMissedClicks--;
                //if (this.gameObject.transform.childCount == 0)
                    particles.PlayParticle(this.gameObject.transform);
                ObjectMovement.routeToGo = findEmptySlot.CheckFirstEmptySlot(Inventory.arraySlots);
                StartCoroutine(objectMovement.GoByTheRoute2(objectMovement.routeTaken));
            }
        }

        private void CalculateMissClicks()
        {
            tMPro = this.gameObject.GetComponentInChildren<TMP_Text>();
            numberOfMissedClicks++;
            //            Debug.Log(numberOfMissedClicks);

            if (rememberClicks % 10 == 0 && rememberClicks != 0 && rememberClicks != rememberLastTimeClicks)
            {
                rememberLastTimeClicks = rememberClicks;
                rememberTime = TimerManager.timeValue;
                TimerManager.timeValue -= 5;
                tMPro.text = "-5s";

            }
        }

        private void OnDrag()
        {
            if (!dragAnDrop.thisObjectIsIn && DialogueManager.isContinueButtonEnabled)
            {
                //OnDrag Find correct placeholder for clicked object 
                dragAnDrop.resetParent = this.transform.parent;
                //resetPostion = this.transform.position;
                resetRotation = this.transform.rotation;
                for (int i = 0; i < SceneLoaded.placeholders.Length; i++)
                {
                    if (SceneLoaded.placeholders[i] != null)
                    {
                        if (this.gameObject.name == SceneLoaded.placeholders[i].name + "X")
                        {
                            dragAnDrop.correctForm = SceneLoaded.placeholders[i];
                            this.transform.rotation = dragAnDrop.correctForm.transform.rotation;
                            index = i;
                            break;
                        }
                    }
                }

                //make object bigger
                this.transform.parent = null;
                transform.localScale = new Vector2(1, 1);


                Vector3 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                dragAnDrop.startPosX = mousePos.x - this.transform.localPosition.x;
                dragAnDrop.startPosY = mousePos.y - this.transform.localPosition.y;

                dragAnDrop.isMoving = true;

                if (TimerManager.timeValue == 0)
                    Destroy(this.gameObject);
            }
        }

        public static bool CheckIfFirstSceneIsActive()
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (sceneIndex % 2 == 0 && sceneIndex != 0)
                return false;
            else if (sceneIndex % 2 != 0 && sceneIndex != 0)
                return true;

            return false;
        }
        private void OnDrop()
        {
            //OnDrop reset position if its wrong object on wrong position or fix in placeholder if its right
            if (Input.GetMouseButtonUp(0) && !CheckIfFirstSceneIsActive())
            {
                if (!dragAnDrop.thisObjectIsIn && DialogueManager.isContinueButtonEnabled && dragAnDrop.isMoving)
                {
                    dragAnDrop.isMoving = false;

                    if (dragAnDrop.correctForm != null && IsInRangeOfPlaceholder())
                    {
                        PutObjectInPlaceholder();
                    }
                    else
                    {
                        ReturnObjectToInventory();
                    }
                }
            }
        }

        private void ReturnObjectToInventory()
        {
            this.transform.SetParent(dragAnDrop.resetParent);
            if (dragAnDrop.resetParent != null)
            {
                //Debug.Log(dragAnDrop.resetParent);
                this.transform.position = new Vector3(dragAnDrop.resetParent.position.x - dragAnDrop.resetPosition.x, dragAnDrop.resetParent.position.y - dragAnDrop.resetPosition.y, dragAnDrop.resetPosition.z);
            }
            else
                Destroy(this.gameObject);

            if (this.gameObject.name.Contains("Y"))
                transform.localScale = new Vector2(0.4f, 0.4f);
            else if (this.gameObject.name.Contains("W"))
                transform.localScale = new Vector2(0.5f, 0.5f);
            else
                transform.localScale = new Vector2(0.7f, 0.7f);
            this.transform.rotation = resetRotation;
            //this.transform.position = resetPostion;
        }

        private void PutObjectInPlaceholder()
        {
            this.transform.SetParent(dragAnDrop.correctForm.transform);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = this.gameObject.transform.parent.GetComponent<SpriteRenderer>().sprite;
            dragAnDrop.thisObjectIsIn = true;
            DialogueManager.dialogueTrigger.TriggerDialogue(index);

            this.transform.position = new Vector3(dragAnDrop.correctForm.transform.position.x, dragAnDrop.correctForm.transform.position.y, dragAnDrop.correctForm.transform.position.z);
            this.transform.rotation = dragAnDrop.correctForm.transform.rotation;
            this.transform.localScale = new Vector2(1, 1);
            dragAnDrop.isFinished = true;
            DragAnDrop.numberOfPartsIn++;
        }

        private bool IsInRangeOfPlaceholder()
        {
            return Mathf.Abs(this.transform.localPosition.x - dragAnDrop.correctForm.transform.localPosition.x) <= 0.7f &&
                    Mathf.Abs(this.transform.localPosition.y - dragAnDrop.correctForm.transform.localPosition.y) <= 0.7f;
        }
    }
}