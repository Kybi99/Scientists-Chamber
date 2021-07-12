using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FourGear.Singletons
{
    public class Object4 : MonoBehaviour
    {
        private static Object4 objectInstance4;
        void Start()
        {
            if (objectInstance4 != null)
            {
                Destroy(this.gameObject);
                return;
            }

            objectInstance4 = this;
        }

    }
}