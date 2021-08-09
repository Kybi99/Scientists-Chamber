using UnityEngine;
using UnityEngine.SceneManagement;
using FourGear.Singletons;
using FourGear.UI;
using FourGear.Dialogue;
namespace FourGear.Mechanics
{
    public class OnMouseEvents : MonoBehaviour
    {
        [SerializeField] private FindEmptySlot findEmptySlot;
        [SerializeField] private ObjectPath objectPath;
        [SerializeField] private DragAnDrop dragAnDrop;
        private int index;
                    
        //OnClickFuntions
        public void OnMouseDown()
        {
            if (Input.GetMouseButtonDown(0) && (SceneManager.GetActiveScene().name == "Skladiste" || SceneManager.GetActiveScene().name == "PupinSkladiste" ||
                SceneManager.GetActiveScene().name == "TeslaSkladiste"))
            {
                if (!objectPath.inInventory && ObjectPath.coroutineAllowed && ObjectPath.routeToGo < Inventory.arraySlots.Length && ShowHint.canClick)
                {
                    Particles.PlayParticle(this.gameObject.transform);
                    ObjectPath.routeToGo = findEmptySlot.CheckFirstEmptySlot(Inventory.arraySlots);
                    if (ObjectPath.routeToGo != -1)
                        StartCoroutine(objectPath.GoByTheRoute(ObjectPath.routeToGo));
                }

                else if (objectPath.inInventory && ObjectPath.coroutineAllowed && ShowHint.canClick)
                {
                    Particles.PlayParticle(this.gameObject.transform);
                    ObjectPath.routeToGo = findEmptySlot.CheckFirstEmptySlot(Inventory.arraySlots);
                    StartCoroutine(objectPath.GoByTheRoute2(objectPath.routeTaken));
                }
            }

            //DragAndDrop                                                                          
            else if (Input.GetMouseButtonDown(0) && !dragAnDrop.thisObjectIsIn && DialogueManager.isContinueButtonEnabled &&
                (SceneManager.GetActiveScene().name == "Radna soba" || SceneManager.GetActiveScene().name == "PupinRadnaSoba" || SceneManager.GetActiveScene().name == "TeslaRadnaSoba"))
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
        private void OnMouseUp()
        {
            //OnDrop reset position if its wrong object on wrong position or fix in placeholder if its right
            if (Input.GetMouseButtonUp(0) && !dragAnDrop.thisObjectIsIn && DialogueManager.isContinueButtonEnabled && (SceneManager.GetActiveScene().name == "Radna soba" ||
                SceneManager.GetActiveScene().name == "PupinRadnaSoba" || SceneManager.GetActiveScene().name == "TeslaRadnaSoba"))
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
