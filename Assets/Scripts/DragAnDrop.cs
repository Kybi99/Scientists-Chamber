using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DragAnDrop : MonoBehaviour
{
    //private GameObject[] correctForms;
    private GameObject correctForm;
    private Transform resetParent;
    //[HideInInspector] public static GameObject[] placeholders;
    //private GameObject[] objects;
    private bool isMoving;
    private bool isFinished;
    //private static int j;
    private float startPosX;
    private float startPosY;
    private Vector3 resetPosition;
   
    private void Start()
    {  
        resetPosition = this.transform.localPosition;
        //Debug.Log(this.transform.parent);
       
    }
    private void Update()
    {
        if(!isFinished)
        {
            if(isMoving)
            {
                Vector3 mousePos;                                                                                                                                       //get mouse position
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.localPosition.z);           //change object position (follow the mouse)
            }
        }
       
    }
    private void OnMouseDown()
    {
        resetParent =  this.transform.parent;
        if(Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name == "Radna soba")                                                                                   //OnDrag Find correct placeholder for clicked object 
        {
            
            for(int i = 0; i < NextScene.placeholders.Length; i++)
            {
                if(NextScene.placeholders[i] != null)
                {
                    if(this.gameObject.name == NextScene.placeholders[i].name)
                    {
                        correctForm = NextScene.placeholders[i];
                        break;
                    }
                   /* else
                        correctForm = resetParent.gameObject; */  
                }    
            }
                
            //Debug.Log(correctForm);
            this.transform.parent = null;
            transform.localScale = new Vector2(0.5f, 0.5f);                                                                                                                     //make object bigger

            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x; 
            startPosY = mousePos.y - this.transform.localPosition.y; 

            isMoving = true;
        }
    }

    private void OnMouseUp()                                                                                                                                                    //OnDrop reset position if its wrong object on wrong position or fix in placeholder if its right
    {
        
        if(Input.GetMouseButtonUp(0) && SceneManager.GetActiveScene().name == "Radna soba")
        {
            isMoving = false;
            
            
            if(correctForm != null && Mathf.Abs(this.transform.localPosition.x - correctForm.transform.localPosition.x) <= 0.5f &&
            Mathf.Abs(this.transform.localPosition.y - correctForm.transform.localPosition.y) <= 0.5f)
            {
                this.transform.parent = correctForm.transform;
                this.transform.position = new Vector3(correctForm.transform.position.x, correctForm.transform.position.y, correctForm.transform.position.z);
                isFinished = true;
            }
            else
            {
                this.transform.parent = resetParent;
                this.transform.localPosition = new Vector3(resetPosition.x, resetPosition.y, resetPosition.z);
                transform.localScale = new Vector2(0.25f, 0.25f);
            }
        }

       
    }
}
