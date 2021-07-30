using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FourGear.UI;
using FourGear.Mechanics;
using FourGear.Singletons;
namespace FourGear
{
    public class SceneLoaded : MonoBehaviour
    {
        int i;
        [SerializeField] private Texture2D cursorTexture;
        private CursorMode cursorMode;
        private Vector2 hotSpot;
        public static GameObject[] placeholders;

        void Start()
        {
            cursorMode = CursorMode.ForceSoftware;
            i = 0;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            placeholders = GameObject.FindGameObjectsWithTag("placeholders");

            if (scene.name == "Radna soba" || scene.name == "PupinRadnaSoba" || scene.name == "TeslaRadnaSoba")
            {
                PrepareSceneRadnaSoba();
            }
            else if (scene.name == "Skladiste" || scene.name == "PupinSkladiste" || scene.name == "TeslaSkladiste")
                PrepareSceneSkladiste();
        }

        private void PrepareSceneRadnaSoba()
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
            for (int i = 0; i < placeholders.Length; i++)
            {
                if (placeholders[i] != null)
                {
                    placeholders[i].GetComponent<SpriteRenderer>().enabled = true;                                                                  //activate placeholders
                    foreach (Transform child in placeholders[i].transform)
                    {
                        child.GetComponent<SpriteRenderer>().enabled = true;
                    }
                }

            }
            for (int i = 0; i < Objects.framedObjects.Length; i++)
            {
                if (Objects.framedObjects[i] != null)
                    Objects.framedObjects[i].gameObject.SetActive(false);
            }

            ChangeCursor();

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

            for (i = 0; i < placeholders.Length; i++)
            {
                if (placeholders[i] != null)
                {
                    placeholders[i].GetComponent<SpriteRenderer>().enabled = false;
                    foreach (Transform child in placeholders[i].transform)
                    {
                        child.GetComponent<SpriteRenderer>().enabled = false;
                        child.GetComponent<BoxCollider2D>().enabled = false;
                    }
                }

            }

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

            for (int i = 0; i < Objects.framedObjects.Length; i++)
            {
                if (Objects.framedObjects[i] != null)
                    Objects.framedObjects[i].gameObject.SetActive(true);
            }
            ChangeCursor();

            DontDestroyOnLoadManager.ChangeNames();
        }
        private void ChangeCursor()
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
    }

}
