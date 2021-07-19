using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FourGear.Mechanics;

namespace FourGear.UI
{
    public class NextScene : MonoBehaviour
    {

        public static GameObject[] objects;
        public static GameObject[] placeholders;
        public static GameObject[] otherObjects;

        public void Start()
        {
            if (placeholders == null)
            {
                placeholders = GameObject.FindGameObjectsWithTag("placeholders");

            }
            for (int i = 0; i < placeholders.Length; i++)
            {
                placeholders[i].transform.gameObject.SetActive(false);
            }
        }
        public void LoadNextScene()
        {
            AsyncOperation op = SceneManager.LoadSceneAsync("Radna Soba");
        
            //Get objects from scene
            objects = GameObject.FindGameObjectsWithTag("objects");
            otherObjects = GameObject.FindGameObjectsWithTag("otherObjects");

            for (int i = 0; i < otherObjects.Length; i++)
            {
                if (otherObjects[i] != null && otherObjects[i].GetComponent<ObjectPath>().inInventory)
                {
                    otherObjects[i].GetComponent<ObjectPath>().enabled = false;                                                         //change active script
                    otherObjects[i].GetComponent<DragAnDrop>().enabled = true;
                }
                else if (otherObjects[i] != null && !otherObjects[i].GetComponent<ObjectPath>().inInventory)
                {
                    otherObjects[i].gameObject.SetActive(false);                                                                        //deactivate incorrect objects in next scene
                }
            }
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] != null && objects[i].GetComponent<ObjectPath>().inInventory)
                {
                    objects[i].GetComponent<ObjectPath>().enabled = false;
                    objects[i].GetComponent<DragAnDrop>().enabled = true;
                }
                else if (objects[i] != null && !objects[i].GetComponent<ObjectPath>().inInventory)
                {
                    objects[i].gameObject.SetActive(false);
                }
            }
            for (int i = 0; i < placeholders.Length; i++)
            {
                if (placeholders[i] != null)
                    placeholders[i].transform.gameObject.SetActive(true);                                                                  //activate placeholders
            }
        }
    }
}
