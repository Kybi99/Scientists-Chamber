using UnityEngine;
using UnityEngine.SceneManagement;


namespace FourGear.UI
{
    public class NextScene : MonoBehaviour
    {
        public static GameObject[] objects;
        public static GameObject[] otherObjects;
        public void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            //Get objects from scene
            objects = GameObject.FindGameObjectsWithTag("objects");
            otherObjects = GameObject.FindGameObjectsWithTag("otherObjects");

        }
    }
}
