using UnityEngine;

namespace FourGear
{
    public class ParticlesSave : MonoBehaviour
    {
         public static ParticlesSave particleInstance;
        void Start()
        {
            particleInstance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }
}
