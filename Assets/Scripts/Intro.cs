using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FourGear
{
    public class Intro : MonoBehaviour
    {
        private float waitTime = 6;
        void Start()
        {
            StartCoroutine(WaitForIntro());
        }

        IEnumerator WaitForIntro()
        {
            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene(1);
        }
    }
}