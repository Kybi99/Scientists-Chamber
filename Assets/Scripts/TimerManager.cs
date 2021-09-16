using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using FourGear.UI;
using FourGear.Mechanics;
namespace FourGear
{
    public class TimerManager : MonoBehaviour
    {
        public static int timeOnStart;
        public static bool gameHasEnded;
        private float timeOnEndScreen;
        private TMP_Text endScreenTime;
        public static TimerManager timerInstance { get; private set; }
        public static float timeValue;
        public static bool timeIsRunning;
        public static Animator endScreenAnimator;
        [SerializeField] private TMP_Text timerText;
        void Start()
        {
            gameHasEnded = false;
            timeOnStart = 300;
            timeValue = timeOnStart;
            timeIsRunning = false;
            //endScreenAnimator = GameObject.FindGameObjectWithTag("endScreen").GetComponent<Animator>();
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
        void Update()
        {
            if (timeValue > 0 && timeIsRunning)
                timeValue -= Time.deltaTime;
            else if (timeValue <= 0 && timeIsRunning)
                timeValue = 0;


            if (timeValue <= 0 && SceneManager.GetActiveScene().buildIndex != 0)
            {
                endScreenAnimator = GameObject.FindGameObjectWithTag("timeUpEndScreen").GetComponent<Animator>();
                endScreenAnimator.Play("EndScreenFadeIn");
                //gameHasEnded = true;
                //DisplayTime(0);
                ShowHint.canClick = false;
                ShowHint.canShowHint = false;
            }
            DisplayTime(timeValue);
        }
        public void DisplayTime(float timeToDisplay)
        {
            if (timeToDisplay < 0)
            {
                timeToDisplay = 0;
            }

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            /*if (gameHasEnded && timeToDisplay == 0)
            {
                endScreenTime = GameObject.FindGameObjectWithTag("timeUpEndScreen").GetComponent<TMP_Text>();
                endScreenTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }*/
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        public void DisplayEndTime(float timeToDisplay)
        {
            if (timeToDisplay < 0)
            {
                timeToDisplay = 0;
            }

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            float miliseconds = timeToDisplay * 100;
            miliseconds = Mathf.FloorToInt(miliseconds % 100);

            if (gameHasEnded && timeToDisplay != 0)
            {
                endScreenTime = GameObject.FindGameObjectWithTag("endScreenImage").GetComponentInChildren<TMP_Text>();
                endScreenTime.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, miliseconds);
            }

        }
    }
}
