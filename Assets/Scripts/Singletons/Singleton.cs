/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourGear
{
    public class Singleton
    {
        private volatile static Singleton instance; // Note volatile keyword.

        private Singleton() { }

        public static Singleton getInstance()
        {
            if (instance == null)
            {
                synchronized(Singleton.class) 
                {
                    if (instance == null) {
                        instance = new Singleton();
                    }
                }
            }
            return instance;
        }
        public MyObject createMyObject() { // Factory method.
            return new MyObject();
        }

    }
   
   
}
*/