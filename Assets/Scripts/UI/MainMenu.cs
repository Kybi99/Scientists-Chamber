using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FourGear.ScriptableObjects;

namespace FourGear.UI
{
    public class MainMenu : MonoBehaviour
    {

        [SerializeField] private Button[] levelButtons;
        [SerializeField] private GameObject levelInfo;
        [SerializeField] private Animator animator;
        [SerializeField] private InfoDisplay infoDisplay;
        [SerializeField] private Texture2D cursorTexture;
        private CursorMode cursorMode;
        private int indexOfButtonClicked;
        private bool flag;

        private void Start()
        {
            cursorMode = CursorMode.ForceSoftware;
            Cursor.SetCursor(cursorTexture, new Vector2(0, 3), cursorMode);

            animator = levelInfo.gameObject.GetComponent<Animator>();

            for (int i = 0; i < levelButtons.Length; i++)
            {
                int closureIndex = i;                                                               //zbog izbegavanja errora 
                levelButtons[closureIndex].onClick.AddListener(() => TaskOnClick(closureIndex)); //Na klik dugmeta uradi task
            }
        }

        public void TaskOnClick(int buttonIndex)
        {
            indexOfButtonClicked = infoDisplay.WriteCorrectDataOnCanvas(buttonIndex);               //poziva fju iz klase InfoDisplay koja postavlja vrednosti u svoje promenljive u zavisnosti od indexa dugmeta na koje je kliknuto
            //sideButtons[i].gameObject.SetActive(true);
            animator.Play("Transformation");                                                         //info image coming into scene
            flag = false;
            ButtonsOnOff();                                                                          //ako je flag true ukljuci level buttone i iskljuci side buttone, else iskljuci level buttone
        }
        public void TaskOnClick2()
        {
            animator.Play("Reverse");                                                                 //info image leaving scene
            flag = true;
            ButtonsOnOff();
        }

        public void ButtonsOnOff()
        {

            for (int i = 0; i < levelButtons.Length; i++)
            {
                if (flag == false)
                    levelButtons[i].interactable = false;
                else
                {
                    levelButtons[i].interactable = true;
                }
            }
        }

        public void LoadLevel()
        {
            //load specific level
            if (indexOfButtonClicked == 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1 + indexOfButtonClicked);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2 + indexOfButtonClicked);
        }
    }
}
