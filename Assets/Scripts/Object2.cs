using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object2 : MonoBehaviour
{
    private static Object2 objectInstance2;
    void Start()
    {
        if(objectInstance2 != null)
        {
            Destroy(this.gameObject);
            return;
        }
              
        objectInstance2 = this;
    }
   
}
