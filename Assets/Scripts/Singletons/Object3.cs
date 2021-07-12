using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FourGear.Singletons
{
    public class Object3 : MonoBehaviour
    {
        private static Object3 objectInstance3;
        void Start()
        {
            if (objectInstance3 != null)
            {
                Destroy(this.gameObject);
                return;
            }

            objectInstance3 = this;
        }


    }
}