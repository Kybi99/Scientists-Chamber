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
            workshopLight = this.gameObject.GetComponent<Light2D>();
            workshopLight.enabled = true;
        }
    }
}
