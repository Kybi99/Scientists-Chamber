using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourGear
{
    public class Particles : MonoBehaviour
    {
        private static GameObject explosion;
        private static GameObject trail;
        void Start()
        {
            trail = GameObject.FindGameObjectWithTag("particle");
        }
        public static void PlayParticle(Transform objectTransform)
        {
            explosion = Instantiate(trail, objectTransform.position, Quaternion.identity);
            explosion.GetComponent<ParticleSystem>().Play();
            explosion.transform.parent = objectTransform;

        }
        public static void RestartParticles()
        {
            explosion.GetComponent<ParticleSystem>().Simulate(5, true, true);
            Destroy(explosion);
        }

    }
}
