using UnityEngine;
using UnityEngine.UI;

namespace FourGear
{
    public class FPS : MonoBehaviour
    {

        private int frameCounter = 0;
        private float timeCounter = 0.0f;
        private float lastFramerate = 0.0f;
        public float refreshTime = 0.5f;
        public Text text;


        void Update()
        {
            if (timeCounter < refreshTime)
            {
                timeCounter += Time.deltaTime;
                frameCounter++;
            }
            else
            {
                lastFramerate = (float)frameCounter / timeCounter;
                frameCounter = 0;
                timeCounter = 0.0f;
            }

            text.text = lastFramerate.ToString("F2");
        }
    }
}
