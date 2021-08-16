using UnityEngine;
using UnityEngine.UI;

namespace FourGear
{
    public class FPS : MonoBehaviour
    {

        private int m_frameCounter = 0;
        private float m_timeCounter = 0.0f;
        private float m_lastFramerate = 0.0f;
        public float m_refreshTime = 0.5f;
        public Text text;


        void Update()
        {
            if (m_timeCounter < m_refreshTime)
            {
                m_timeCounter += Time.deltaTime;
                m_frameCounter++;
            }
            else
            {
                m_lastFramerate = (float)m_frameCounter / m_timeCounter;
                m_frameCounter = 0;
                m_timeCounter = 0.0f;
            }

            text.text = m_lastFramerate.ToString("F2");
        }
    }
}
