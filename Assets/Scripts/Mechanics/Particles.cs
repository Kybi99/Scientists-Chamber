using UnityEngine;

namespace FourGear.Mechanics
{
    public class Particles : MonoBehaviour
    {
        private GameObject effect;
        private GameObject effect2;
        private ParticleSystem particleSystem1;
        private ParticleSystem particleSystem2;
        private GameObject trail;
        private GameObject particles;

        void Start()
        {
            trail = GameObject.FindGameObjectWithTag("particle");
            particles = GameObject.FindGameObjectWithTag("secondParticle");
        }
        public void PlayParticle(Transform objectTransform)
        {
            effect = Instantiate(trail, objectTransform.position, Quaternion.identity);
            particleSystem1 = effect.GetComponent<ParticleSystem>();
            particleSystem1.Play();
            effect.transform.parent = objectTransform;

            effect2 = Instantiate(particles, objectTransform.position, Quaternion.identity);
            particleSystem2 = effect2.GetComponent<ParticleSystem>();
            particleSystem2.Play();
            effect2.transform.parent = objectTransform;
        }
        public void PauseParticles(ParticleSystem[] particleSystem)
        {
            for (int i = 0; i < particleSystem.Length; i++)
            {
                if (particleSystem[i] != null)
                {
                    particleSystem[i].Pause();
                }
            }
        }
        public void ResumeParticles(ParticleSystem[] particleSystem)
        {
            for (int i = 0; i < particleSystem.Length; i++)
            {
                if (particleSystem[i] != null)
                {
                    particleSystem[i].Play();
                }
            }
        }


        public void RestartParticles(ParticleSystem[] particleSystem)
        {
            for (int i = 0; i < particleSystem.Length; i++)
            {
                if (particleSystem[i] != null)
                {
                    particleSystem[i].Stop();
                    particleSystem[i].Simulate(0.4f, true, true);;
                }
            }
            /*effect.GetComponent<ParticleSystem>().Stop();
            effect2.GetComponent<ParticleSystem>().Stop();
            effect.GetComponent<ParticleSystem>().Simulate(0, true, true);
            effect2.GetComponent<ParticleSystem>().Simulate(0, true, true);*/

            /* Destroy(effect);
             Destroy(effect2);*/
        }

    }
}
