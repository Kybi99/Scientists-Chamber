using UnityEngine;
using FourGear.Mechanics;

namespace FourGear
{
    public class HighlightAll : MonoBehaviour
    {
        private Animator globalLineLightAnimator;
        private float rememberTime;
        void Start()
        {
            globalLineLightAnimator = this.gameObject.GetComponent<Animator>();
        }

        void Update()
        {
            if (Mathf.Round(TimerManager.timeValue % 33) == 0 && TimerManager.timeValue < 299 && FramedObjects.isHighLightAllowed)
            {
                globalLineLightAnimator.SetBool("isReadyToPlay", true);
                rememberTime = TimerManager.timeValue;
            }
            else if (rememberTime - TimerManager.timeValue > 2f)
                globalLineLightAnimator.SetBool("isReadyToPlay", false);
        }
    }
}
