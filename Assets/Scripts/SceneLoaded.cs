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
        private Vector2 hotSpot;
        private string sceneName;
        private string sceneName2;
        public static GameObject[] placeholders;
        public static GameObject[] objects;
        public static GameObject[] otherObjects;

        void Start()
        {
            //Get objects from scene

            objects = GameObject.FindGameObjectsWithTag("objects");
            otherObjects = GameObject.FindGameObjectsWithTag("otherObjects");

            i = 0;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {

            placeholders = GameObject.FindGameObjectsWithTag("placeholders");
            string last9Letters = OnMouseEvents.CheckLast9LettersOfSceneName();

            if (last9Letters == OnMouseEvents.sceneName2)
                PrepareSceneRadnaSoba();
            else if (last9Letters == OnMouseEvents.sceneName)
                PrepareSceneSkladiste();
            else if (last9Letters == "Main menu")
                PrepareSceneMainMenu();
        }

        private void PrepareSceneRadnaSoba()
        {
            ShowHint.isFirstTimeInScene = false;

            for (int i = 0; i < otherObjects.Length; i++)
            {
                if (otherObjects[i] != null && otherObjects[i].GetComponent<ObjectMovement>().inInventory)
                {
                    otherObjects[i].GetComponent<ObjectMovement>().enabled = false;
                    //change active script
                    otherObjects[i].GetComponent<DragAnDrop>().enabled = true;
                }
                else if (otherObjects[i] != null && !otherObjects[i].GetComponent<ObjectMovement>().inInventory)
                {
                    otherObjects[i].gameObject.SetActive(false);
                    //deactivate incorrect objects in next scene
                }
            }
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] != null && objects[i].GetComponent<ObjectMovement>().inInventory)
                {
                    objects[i].GetComponent<ObjectMovement>().enabled = false;
                    objects[i].GetComponent<DragAnDrop>().enabled = true;
                }
                else if (objects[i] != null && !objects[i].GetComponent<ObjectMovement>().inInventory)
                {
                    objects[i].gameObject.SetActive(false);
                }
            }
            for (int i = 0; i < placeholders.Length; i++)
            {
                if (placeholders[i] != null)
                {
                    placeholders[i].GetComponent<SpriteRenderer>().enabled = true;

                    //activate placeholders
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

            for (i = 0; i < objects.Length; i++)
            {
                if (objects[i] != null)
                {
                    objects[i].GetComponent<ObjectMovement>().enabled = true;
                    objects[i].GetComponent<DragAnDrop>().enabled = false;
                    if (objects[i].gameObject.activeSelf == false)
                        objects[i].gameObject.SetActive(true);
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
            for (i = 0; i < otherObjects.Length; i++)
            {
                if (otherObjects[i] != null)
                {
                    otherObjects[i].GetComponent<ObjectMovement>().enabled = true;
                    otherObjects[i].GetComponent<DragAnDrop>().enabled = false;
                    if (otherObjects[i].gameObject.activeSelf == false)
                        otherObjects[i].gameObject.SetActive(true);
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
            CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.Arrow);
        }
        private void PrepareSceneMainMenu()
        {
            OnMouseEvents.numberOfMissedClicks = 0;
            
            GameObject[] ddols = GameObject.FindGameObjectsWithTag("DDOLs");
            GameObject inventory = GameObject.FindGameObjectWithTag("inventory");
            GameObject timer = GameObject.FindObjectOfType<TimerManager>().gameObject;
            DontDestroyOnLoadManager.DestroyAll();
            foreach (GameObject ddol in ddols)
                Destroy(ddol);

            Destroy(inventory);
            Destroy(timer);

            DragAnDrop.numberOfPartsIn = 0;
            ShowHint.isFirstTimeInScene = true;
        }
    }

}
