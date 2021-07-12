using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FourGear.Singletons
{
    public class Object5 : MonoBehaviour
    {
        private static Object5 objectInstance;
        void Start()
        {

            if (objectInstance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            objectInstance = this;
        }

    }

}