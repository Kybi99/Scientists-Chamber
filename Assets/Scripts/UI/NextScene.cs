using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace FourGear.UI
{
    public class NextScene : MonoBehaviour
    {

        public static GameObject[] objects;
 
        public static GameObject[] otherObjects;

        public void Start()
        {
           /* if (placeholders == null)
            {
                placeholders = GameObject.FindGameObjectsWithTag("placeholders");

            }
            for (int i = 0; i < placeholders.Length; i++)
            {
                placeholders[i].GetComponent<SpriteRenderer>().enabled = false;
            }*/
        }
        public void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            //Get objects from scene
            objects = GameObject.FindGameObjectsWithTag("objects");
            otherObjects = GameObject.FindGameObjectsWithTag("otherObjects");

        }
    }
}
