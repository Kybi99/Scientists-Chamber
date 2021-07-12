using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FourGear.Singletons
{
    public class Placeholders : MonoBehaviour
    {
        public static Placeholders phInstance;
        void Start()
        {
            if (phInstance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            phInstance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }

    }

}
