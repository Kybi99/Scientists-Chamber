using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FourGear.Mechanics;
namespace FourGear.Singletons
{
    public class DoorDDOL : MonoBehaviour
    {
        private static DoorDDOL framedObjectInstance;
        void Start()
        {

            if (framedObjectInstance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            framedObjectInstance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }

    }
}
