using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using FourGear.UI;
using FourGear.Mechanics;
namespace FourGear
{
    public class TimerManager : MonoBehaviour
    {
        public static TimerManager timerInstance { get; private set; }
        public static float timeValue;
        public static bool timeIsRunning;
        public static Animator endScreenAnimator;
        [SerializeField] private TMP_Text timerText;
        void Start()
        {
            timeValue = 300;
            timeIsRunning = true;
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
            else if(timeValue <= 0 && timeIsRunning)
                timeValue = 0;

            if (timeValue <= 0 && SceneManager.GetActiveScene().name != "Main menu")
            {
                endScreenAnimator = GameObject.FindGameObjectWithTag("timeUpEndScreen").GetComponent<Animator>();
                endScreenAnimator.Play("EndScreenFadeIn");
                ShowHint.canClick = false;
                ShowHint.canShowHint = false;
            }
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
