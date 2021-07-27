using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FourGear.Mechanics;
using FourGear.Dialogue;
namespace FourGear.UI
{

    public class PreviousScene : MonoBehaviour
    {

        //public static bool isComingHome;
        private void Start()
        {
            //isComingHome = false;
            //framedObjects = GetComponent<FramedObjects>();
        }
        public void LoadPreviousScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

}
