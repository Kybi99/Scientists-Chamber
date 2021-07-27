using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourGear
{
    public class Particles : MonoBehaviour
    {
        private static GameObject effect;
        private static GameObject trail;
        void Start()
        {
            trail = GameObject.FindGameObjectWithTag("particle");
        }
        public static void PlayParticle(Transform objectTransform)
        {
            effect = Instantiate(trail, objectTransform.position, Quaternion.identity);
            effect.GetComponent<ParticleSystem>().Play();
            effect.transform.parent = objectTransform;
        }
        public static void RestartParticles()
        {
            effect.GetComponent<ParticleSystem>().Simulate(0, true, true);
            Destroy(effect);
        }

    }
}
