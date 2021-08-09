using UnityEngine;
using UnityEngine.SceneManagement;

namespace FourGear.UI
{

    public class PreviousScene : MonoBehaviour
    {
        public void LoadPreviousScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

}
