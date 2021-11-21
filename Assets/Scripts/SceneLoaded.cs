using UnityEngine;
using UnityEngine.SceneManagement;
using FourGear.UI;
using FourGear.Mechanics;
using FourGear.Singletons;
using FourGear.Dialogue;

namespace FourGear
{
    public class SceneLoaded : MonoBehaviour
    {
        private int i;
        private int children;
        private static Transform placeholder;
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
            
            if (SceneManager.GetActiveScene().buildIndex == 1)
                PrepareSceneMainMenu();
            else if (!OnMouseEvents.CheckIfFirstSceneIsActive())
                PrepareSceneRadnaSoba();
            else if (OnMouseEvents.CheckIfFirstSceneIsActive())
                PrepareSceneSkladiste();

        }

        private void PrepareSceneRadnaSoba()
        {
            placeholder = FindObjectOfType<PlaceholdersDDOL>().transform;
            children = placeholder.childCount;
            placeholders = new GameObject[placeholder.childCount];
            for (int i = 0; i < children; i++)
            {
                placeholders[i] = placeholder.GetChild(i).gameObject;
            }

            //ShowHint.isFirstTimeInScene = false;

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

            if(placeholders != null)
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
            ObjectMovement.numberOfObjectsFlying = 0;
            ObjectMovement.isNextSceneAllowed = true;

            OnMouseEvents.numberOfMissedClicks = 0;
            if (Objects.otherObjects != null || Objects.objects != null)
            {
                for (int i = 0; i < Objects.otherObjects.Length; i++)
                    Destroy(Objects.otherObjects[i]);
                for (int i = 0; i < Objects.objects.Length; i++)
                    Destroy(Objects.objects[i]);
            }

            GameObject[] ddols = GameObject.FindGameObjectsWithTag("DDOLs");
            GameObject inventory = GameObject.FindGameObjectWithTag("inventory");
            GameObject timer = FindObjectOfType<TimerManager>().gameObject;
            ParticlesSave[] particles = FindObjectsOfType<ParticlesSave>();
            GameObject audioSave = FindObjectOfType<AudioSave>().gameObject;
            DontDestroyOnLoadManager.DestroyAll();
            foreach (GameObject ddol in ddols)
                Destroy(ddol);
            foreach (ParticlesSave particle in particles)
                Destroy(particle.gameObject);
            Destroy(inventory);
            Destroy(timer);
            Destroy(audioSave);

            DragAnDrop.numberOfPartsIn = 0;
            ShowHint.isFirstTimeInScene = true;
        }
    }

}
