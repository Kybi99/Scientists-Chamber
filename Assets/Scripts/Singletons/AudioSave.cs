using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourGear
{
    public class AudioSave : MonoBehaviour
    {
        public static AudioSave audioSave;
        void Awake()
        {
            if (audioSave != null)
            {
                Destroy(this.gameObject);
                return;
            }

            audioSave = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }
}
