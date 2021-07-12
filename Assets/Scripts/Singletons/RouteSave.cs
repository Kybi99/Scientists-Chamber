using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FourGear.Singletons
{
    public class RouteSave : MonoBehaviour
    {
        public static RouteSave routeInstance;
        void Start()
        {
            /*if(routeInstance != null)
            {
                Destroy(this.gameObject);
                return;
            }*/

            routeInstance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }
}

