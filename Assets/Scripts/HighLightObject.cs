using UnityEngine;

namespace FourGear
{
    public class HighLightObject : MonoBehaviour
    {
        private Animator doorAnimator;
        void Start()
        {
            doorAnimator = this.gameObject.GetComponent<Animator>();
        }

        void Update()
        {
            if (TimerManager.timeValue % 10 == 0)
                doorAnimator.SetBool("isReadyToPlay", true);
            /*else if (doorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                doorAnimator.SetBool("isReadyToPlay", false);*/

        }
    }
}
