using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreviousScene : MonoBehaviour
{
    public void LoadPreviousScene(){

        //Change active scripts on objects and activate deactivated objects when coming back to first room 
        for(int i = 0; i < NextScene.objects.Length; i++)
        {
            if(NextScene.objects[i] != null)
            {
                NextScene.objects[i].GetComponent<ObjectPath>().enabled = true;
                NextScene.objects[i].GetComponent<DragAnDrop>().enabled = false; 
                if(NextScene.objects[i].gameObject.activeSelf == false)
                    NextScene.objects[i].gameObject.SetActive(true);
            }  
        }
        
        //Deactivate placeholders
        for( int i = 0; i < NextScene.placeholders.Length; i++)
        {
            if( NextScene.placeholders[i] != null)
                NextScene.placeholders[i].transform.gameObject.SetActive(false);
        }   

        //Change active scripts on incorrect objects and activate deactivated objects when coming back to first room
        for(int i = 0; i < NextScene.otherObjects.Length; i++)
        {
            if(NextScene.otherObjects[i] != null)
            {
                NextScene.otherObjects[i].GetComponent<ObjectPath>().enabled = true;
                NextScene.otherObjects[i].GetComponent<DragAnDrop>().enabled = false; 
                if(NextScene.otherObjects[i].gameObject.activeSelf == false)
                    NextScene.otherObjects[i].gameObject.SetActive(true);
            }  
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        
    }
}
