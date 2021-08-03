using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourGear
{
    public class Particles : MonoBehaviour
    {
        private static GameObject effect;
        private static GameObject effect2;
        private static GameObject trail;
        private static GameObject trail2;

        void Start()
        {
            trail = GameObject.FindGameObjectWithTag("particle");
            trail2 = GameObject.FindGameObjectWithTag("secondParticle");
        }
        public static void PlayParticle(Transform objectTransform)
        {
            effect = Instantiate(trail, objectTransform.position, Quaternion.identity);
            effect.GetComponent<ParticleSystem>().Play();
            effect.transform.parent = objectTransform;

            effect2 = Instantiate(trail2, objectTransform.position, Quaternion.identity);
            effect2.GetComponent<ParticleSystem>().Play();
            effect2.transform.parent = objectTransform;
        }
        public static void RestartParticles()
        {
            effect.GetComponent<ParticleSystem>().Simulate(1, true, true);
            effect2.GetComponent<ParticleSystem>().Simulate(1, true, true);

            //Destroy(effect);
        }

    }
}
