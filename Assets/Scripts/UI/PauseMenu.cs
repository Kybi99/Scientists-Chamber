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
        public ShowHint showHint;

        private void Update()
        {
            if (ShowHint.canvasGroup != null)
            {
                if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0 && (ShowHint.canvasGroup.alpha == 0 || ShowHint.canvasGroup.alpha == 1))
                {
                    if (gameIsPaused)
                        Resume();
                    else
                        Pause();
                }
            }

            else if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
            {
                if (gameIsPaused)
                    Resume();
                else
                    Pause();
            }
        }
        public void Resume()
        {
            if (showHint.helpScript != null)
                showHint.helpScript.SetActive(true);

            pausemenuUi.SetActive(false);
            if (ShowHint.canvasGroup.alpha == 0)
            {
                ShowHint.canClick = true;
                ShowHint.canShowHint = true;
            }

            //CursorManager.canChangeCursor = true;
            Time.timeScale = 1f;
            gameIsPaused = false;
            Cursor.SetCursor(resetCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
            CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.Arrow);

        }

        public void Pause()
        {
            if (showHint.helpScript != null)
                showHint.helpScript.SetActive(false);

            pausemenuUi.SetActive(true);
            ShowHint.canClick = false;
            ShowHint.canShowHint = false;
            //CursorManager.canChangeCursor = false;
            Time.timeScale = 0f;
            gameIsPaused = true;
            Cursor.SetCursor(resetCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
            CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.Arrow);

        }
        public void LoadMenu()
        {
            Time.timeScale = 1f;
            OnMouseEvents.numberOfMissedClicks = 0;
            gameIsPaused = false;
            SceneManager.LoadScene("Main Menu");
        }
        public void QuitGame()
        {
            Application.Quit();
        }
    }

}
