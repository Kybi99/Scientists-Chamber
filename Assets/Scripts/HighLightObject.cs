using UnityEngine;
using FourGear.Mechanics;

namespace FourGear
{
    public class HighLightObject : MonoBehaviour
    {
        private Animator doorAnimator;
        private float rememberTime;
        void Start()
        {
            doorAnimator = this.gameObject.GetComponent<Animator>();
        }

        void Update()
        {
            if (Mathf.Round(TimerManager.timeValue % 10) == 0 && TimerManager.timeValue < 299 && FramedObjects.isHighLightAllowed)
            {
                doorAnimator.SetBool("isReadyToPlay", true);
                rememberTime = TimerManager.timeValue;
            }
            else if (rememberTime - TimerManager.timeValue > 0.9f)
                doorAnimator.SetBool("isReadyToPlay", false);
        }
    }
}
