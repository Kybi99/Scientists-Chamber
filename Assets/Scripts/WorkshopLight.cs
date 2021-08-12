using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


namespace FourGear
{
    public class WorkshopLight : MonoBehaviour
    {
        private float rememberTime;
        private Light2D workshopLight;
        void Start()
        {
            rememberTime = TimerManager.timeValue;
            workshopLight = this.gameObject.GetComponent<Light2D>();
        }
        void Update()
        {
            if (rememberTime - TimerManager.timeValue > 20)
                workshopLight.enabled = true;

        }
    }
}
