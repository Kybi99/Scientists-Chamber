using UnityEngine;
using UnityEngine.SceneManagement;


namespace FourGear.UI
{
    public class NextScene : MonoBehaviour
    {
        public void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
