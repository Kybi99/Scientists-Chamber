using UnityEngine;
using FourGear.Mechanics;

namespace FourGear
{
    public class HighLightObject : MonoBehaviour
    {
        public Animator doorLightAnimator;
        public Animator framedObjectLightAnimator;
        private float rememberTime;
        private float rememberTime2;


        void Update()
        {
            if (Mathf.Round(TimerManager.timeValue % 10) == 0 && TimerManager.timeValue < 299)
            {
                doorLightAnimator.SetBool("isReadyToPlay", true);
                rememberTime = TimerManager.timeValue;
            }
            else if (rememberTime - TimerManager.timeValue > 0.9f)
                doorLightAnimator.SetBool("isReadyToPlay", false);

            if (Mathf.Round(TimerManager.timeValue % 21) == 0 && TimerManager.timeValue < 299)
            {
                framedObjectLightAnimator.SetBool("isReadyToPlay", true);
                rememberTime2 = TimerManager.timeValue;
            }
            else if (rememberTime2 - TimerManager.timeValue > 2f)
                framedObjectLightAnimator.SetBool("isReadyToPlay", false);

        }
    }
}
