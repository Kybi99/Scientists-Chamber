using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourGear.Dialogue
{
    [System.Serializable]
    public class Story 
    {
        public string nameOfNpc;
        [TextArea(3,10)]
        public string[] sentences;

    }
}
