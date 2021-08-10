using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace FourGear
{
    public class TimerManager : MonoBehaviour
    {
        public static TimerManager timerInstance { get; private set; }
        public static float timeValue;
        [SerializeField] private TMP_Text timerText;
        void Start()
        {
            timeValue = 300;
        }

        private void Awake()
        {
            if (timerInstance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            timerInstance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
        // Update is called once per frame
        void Update()
        {
            if (timeValue > 0)
                timeValue -= Time.deltaTime;
            else
                timeValue = 0;

            DisplayTime(timeValue);
        }
        void DisplayTime(float timeToDisplay)
        {
            if (timeToDisplay < 0)
            {
                timeToDisplay = 0;
            }

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
