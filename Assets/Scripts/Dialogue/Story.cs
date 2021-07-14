using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourGear.Dialogue
{
    [System.Serializable]
    public class Dialogue : MonoBehaviour
    {
        public string NPCname;
        [TextArea(3,10)]
        public string[] sentences;

    }
}
