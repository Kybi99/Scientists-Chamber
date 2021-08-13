using UnityEngine;
using UnityEngine.SceneManagement;
using FourGear.Mechanics;

namespace FourGear.UI
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool gameIsPaused;
        public GameObject pausemenuUi;
        public Texture2D resetCursorTexture;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gameIsPaused)
                    Resume();
                else
                    Pause();


            }
        }
        public void Resume()
        {
            pausemenuUi.SetActive(false);
            ShowHint.canClick = true;
            ShowHint.canShowHint = true;
            CursorManager.canChangeCursor = true;
            Time.timeScale = 1f;
            gameIsPaused = false;
            Cursor.SetCursor(resetCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        }

        public void Pause()
        {
            pausemenuUi.SetActive(true);
            ShowHint.canClick = false;
            ShowHint.canShowHint = false;
            CursorManager.canChangeCursor = false;
            Time.timeScale = 0f;
            gameIsPaused = true;
            Cursor.SetCursor(resetCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        }
        public void LoadMenu()
        {
            Time.timeScale = 1f;
            OnMouseEvents.numberOfMissedClicks = 0;
            SceneManager.LoadScene("Main Menu");
        }
        public void QuitGame()
        {
            Application.Quit();
        }
    }

}
