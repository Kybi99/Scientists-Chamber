using UnityEngine;
using UnityEngine.SceneManagement;
using FourGear.Dialogue;
namespace FourGear.UI
{
    public class PreviousScene : MonoBehaviour
    {
        public DialogueManager dialogueManager;
        public void LoadPreviousScene()
        {
            if (!dialogueManager.continueClick.enabled)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

}
