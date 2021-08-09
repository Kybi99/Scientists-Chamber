using UnityEngine;
using UnityEngine.SceneManagement;

namespace FourGear.UI
{
    public class EndScreen : MonoBehaviour
    {
        public void LoadMainMenu()
        {
            SceneManager.LoadScene("Main menu");
        }
    }
}
