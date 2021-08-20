using UnityEngine;

namespace FourGear.Mechanics
{
    public class Particles : MonoBehaviour
    {
        private static GameObject effect;
        private static GameObject effect2;
        private static ParticleSystem particleSystem1;
        private static ParticleSystem particleSystem2;

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
            particleSystem1 = effect.GetComponent<ParticleSystem>();
            particleSystem1.Play();
            effect.transform.parent = objectTransform;

            effect2 = Instantiate(trail2, objectTransform.position, Quaternion.identity);
            particleSystem2 = effect2.GetComponent<ParticleSystem>();
            particleSystem2.Play();
            effect2.transform.parent = objectTransform;
        }
        public static void PauseParticles()
        {
            particleSystem1.Pause();
            particleSystem2.Pause();
        }
        public static void ResumeParticles()
        {
            particleSystem1.Play();
            particleSystem2.Play();
        }


        public static void RestartParticles()
        {
            effect.GetComponent<ParticleSystem>().Simulate(1, true, true);
            effect2.GetComponent<ParticleSystem>().Simulate(1, true, true);

            Destroy(effect);
            Destroy(effect2);
        }

    }
}
