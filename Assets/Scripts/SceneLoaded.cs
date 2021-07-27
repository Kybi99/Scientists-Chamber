using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FourGear.UI;
using FourGear.Mechanics;

namespace FourGear
{
    public class SceneLoaded : MonoBehaviour
    {
        int i;
        void Start()
        {
            i = 0;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Radna soba")
            {
                PrepareSceneRadnaSoba();
            }
            else if (scene.name == "Skladiste")
                PrepareSceneSkladiste();
        }

        private static void PrepareSceneRadnaSoba()
        {
            for (int i = 0; i < NextScene.otherObjects.Length; i++)
            {
                if (NextScene.otherObjects[i] != null && NextScene.otherObjects[i].GetComponent<ObjectPath>().inInventory)
                {
                    NextScene.otherObjects[i].GetComponent<ObjectPath>().enabled = false;                                                         //change active script
                    NextScene.otherObjects[i].GetComponent<DragAnDrop>().enabled = true;
                }
                else if (NextScene.otherObjects[i] != null && !NextScene.otherObjects[i].GetComponent<ObjectPath>().inInventory)
                {
                    NextScene.otherObjects[i].gameObject.SetActive(false);                                                                        //deactivate incorrect objects in next scene
                }
            }
            for (int i = 0; i < NextScene.objects.Length; i++)
            {
                if (NextScene.objects[i] != null && NextScene.objects[i].GetComponent<ObjectPath>().inInventory)
                {
                    NextScene.objects[i].GetComponent<ObjectPath>().enabled = false;
                    NextScene.objects[i].GetComponent<DragAnDrop>().enabled = true;
                }
                else if (NextScene.objects[i] != null && !NextScene.objects[i].GetComponent<ObjectPath>().inInventory)
                {
                    NextScene.objects[i].gameObject.SetActive(false);
                }
            }
            /*for (int i = 0; i < DragAnDrop.placeholders.Length; i++)
            {
                if (DragAnDrop.placeholders[i] != null)
                    DragAnDrop.placeholders[i].GetComponent<SpriteRenderer>().enabled = true;                                                                  //activate placeholders
                foreach (Transform child in DragAnDrop.placeholders[i].transform)
                {
                    child.GetComponent<SpriteRenderer>().enabled = true;
                }
            }*/
            for (int i = 0; i < FramedObjects.firstFrameObjects.Length; i++)
            {
                if (FramedObjects.firstFrameObjects[i] != null)
                    FramedObjects.firstFrameObjects[i].gameObject.SetActive(false);
            }
            DontDestroyOnLoadManager.ChangeNames();
        }

        private void PrepareSceneSkladiste()
        {

            //Change active scripts on objects and activate deactivated objects when coming back to first room 

            for (i = 0; i < NextScene.objects.Length; i++)
            {
                if (NextScene.objects[i] != null)
                {
                    NextScene.objects[i].GetComponent<ObjectPath>().enabled = true;
                    NextScene.objects[i].GetComponent<DragAnDrop>().enabled = false;
                    if (NextScene.objects[i].gameObject.activeSelf == false)
                        NextScene.objects[i].gameObject.SetActive(true);
                }
            }

            //Deactivate placeholders

            /*for (i = 0; i < DragAnDrop.placeholders.Length; i++)
            {
                if (DragAnDrop.placeholders[i] != null)
                    DragAnDrop.placeholders[i].GetComponent<SpriteRenderer>().enabled = false;
                foreach (Transform child in DragAnDrop.placeholders[i].transform)
                {
                    child.GetComponent<SpriteRenderer>().enabled = false;
                }
            }*/

            //Change active scripts on incorrect objects and activate deactivated objects when coming back to first room
            for (i = 0; i < NextScene.otherObjects.Length; i++)
            {
                if (NextScene.otherObjects[i] != null)
                {
                    NextScene.otherObjects[i].GetComponent<ObjectPath>().enabled = true;
                    NextScene.otherObjects[i].GetComponent<DragAnDrop>().enabled = false;
                    if (NextScene.otherObjects[i].gameObject.activeSelf == false)
                        NextScene.otherObjects[i].gameObject.SetActive(true);
                }
            }
            /*for (i = 0; i < FramedObjects.firstFrameObjects.Length; i++)
            {*/
            for (int i = 0; i < FramedObjects.firstFrameObjects.Length; i++)
            {
                if (FramedObjects.firstFrameObjects[i] != null)
                    FramedObjects.firstFrameObjects[i].gameObject.SetActive(true);
            }
            /*FramedObjects.firstObjectRenderer.enabled = false;
            FramedObjects.secondObjectRenderer.enabled = true;*/

            DontDestroyOnLoadManager.ChangeNames();
        }
    }
}
