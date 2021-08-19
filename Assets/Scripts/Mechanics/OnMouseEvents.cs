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
        [SerializeField] private ObjectPath objectPath;
        [SerializeField] private DragAnDrop dragAnDrop;
        public static int numberOfMissedClicks = 0;
        private int index;
        private int rememberClicks;
        private float rememberTime;
        private TMP_Text tMPro;
        private string backgroundName;
        public static string sceneName;
        public static string sceneName2;

        private void Start()
        {
            sceneName = "Skladiste";
            sceneName2 = "RadnaSoba";
            backgroundName = "Pozadina";
        }
        private void Update()
        {
            if (tMPro != null)
                tMPro.transform.position = Input.mousePosition + new Vector3(-25, 0, 0);
            if (rememberTime - TimerManager.timeValue > 6 || TimerManager.timeValue == 0 && tMPro != null)
                tMPro.text = "";
        }
        //OnClickFuntions
        public void OnMouseDown()
        {
            string last9Letters = CheckLast9LettersOfSceneName();
            sceneName = "Skladiste";
            if (Input.GetMouseButtonDown(0) && last9Letters == sceneName && ShowHint.canClick)
            {
                OnClick();
            }

            //DragAndDrop  
            else if (Input.GetMouseButtonDown(0) && last9Letters == sceneName2)
            {
                OnDrag();
            }
        }
        private void OnMouseUp()
        {
            string last9Letters = CheckLast9LettersOfSceneName();
            OnDrop(last9Letters);
        }
        private void OnClick()
        {
            if (this.gameObject.name == backgroundName)
            {
                rememberClicks = numberOfMissedClicks;
                CalculateMissClicks();
            }
            else if (!objectPath.inInventory && ObjectPath.coroutineAllowed && ObjectPath.routeToGo < Inventory.arraySlots.Length)
            {
                numberOfMissedClicks--;
                Particles.PlayParticle(this.gameObject.transform);
                ObjectPath.routeToGo = findEmptySlot.CheckFirstEmptySlot(Inventory.arraySlots);
                if (ObjectPath.routeToGo != -1)
                    StartCoroutine(objectPath.GoByTheRoute(ObjectPath.routeToGo));
            }

            else if (objectPath.inInventory && ObjectPath.coroutineAllowed)
            {
                numberOfMissedClicks--;
                Particles.PlayParticle(this.gameObject.transform);
                ObjectPath.routeToGo = findEmptySlot.CheckFirstEmptySlot(Inventory.arraySlots);
                StartCoroutine(objectPath.GoByTheRoute2(objectPath.routeTaken));
            }
        }

        private void CalculateMissClicks()
        {
            tMPro = this.gameObject.GetComponentInChildren<TMP_Text>();
            numberOfMissedClicks++;
            Debug.Log(numberOfMissedClicks);

            if (rememberClicks % 10 == 0 && rememberClicks != 0)
            {
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
                for (int i = 0; i < SceneLoaded.placeholders.Length; i++)
                {
                    if (SceneLoaded.placeholders[i] != null)
                    {
                        if (this.gameObject.name == SceneLoaded.placeholders[i].name + "X")
                        {
                            dragAnDrop.correctForm = SceneLoaded.placeholders[i];
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
            }
        }

        public static string CheckLast9LettersOfSceneName()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            string last9Letters = sceneName.Substring(sceneName.Length - 9);
            return last9Letters;
        }
        private void OnDrop(string last9Letters)
        {
            //OnDrop reset position if its wrong object on wrong position or fix in placeholder if its right
            if (Input.GetMouseButtonUp(0) && last9Letters == sceneName2)
            {
                if (!dragAnDrop.thisObjectIsIn && DialogueManager.isContinueButtonEnabled)
                {
                    dragAnDrop.isMoving = false;

                    if (dragAnDrop.correctForm != null && Mathf.Abs(this.transform.localPosition.x - dragAnDrop.correctForm.transform.localPosition.x) <= 0.5f &&
                        Mathf.Abs(this.transform.localPosition.y - dragAnDrop.correctForm.transform.localPosition.y) <= 0.5f)
                    {
                        this.transform.parent = dragAnDrop.correctForm.transform;
                        dragAnDrop.thisObjectIsIn = true;
                        DialogueManager.dialogueTrigger.TriggerDialogue(index);

                        this.transform.position = new Vector3(dragAnDrop.correctForm.transform.position.x, dragAnDrop.correctForm.transform.position.y, dragAnDrop.correctForm.transform.position.z);
                        this.transform.rotation = dragAnDrop.correctForm.transform.rotation;
                        this.transform.localScale = new Vector2(1, 1); ;
                        dragAnDrop.isFinished = true;
                        DragAnDrop.numberOfPartsIn++;
                    }
                    else
                    {
                        this.transform.parent = dragAnDrop.resetParent;
                        this.transform.localPosition = new Vector3(dragAnDrop.resetPosition.x, dragAnDrop.resetPosition.y, dragAnDrop.resetPosition.z);
                        transform.localScale = new Vector2(0.5f, 0.5f);
                    }
                }

            }
        }
    }
}