using UnityEngine;

namespace FourGear.Mechanics
{
    public class Particles : MonoBehaviour
    {
        private GameObject effect;
        private GameObject effect2;
        private ParticleSystem particleSystem1;
        private ParticleSystem particleSystem2;
        public GameObject trail;
        public GameObject trail2;

        void Start()
        {
            trail = GameObject.FindGameObjectWithTag("particle");
            //trail2 = GameObject.FindGameObjectWithTag("secondParticle");
        }
        public void PlayParticle(Transform objectTransform)
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
        public void PauseParticles()
        {
            if (particleSystem1 != null && particleSystem2 != null)
            {
                particleSystem1.Pause();
                particleSystem2.Pause();
            }

        }
        public void ResumeParticles()
        {
            if (particleSystem1 != null && particleSystem2 != null)
            {
                particleSystem1.Play();
                particleSystem2.Play();
            }

        }


        public void RestartParticles()
        {
            effect.GetComponent<ParticleSystem>().Stop();
            effect2.GetComponent<ParticleSystem>().Stop();
            effect.GetComponent<ParticleSystem>().Simulate(1, true, true);
            effect2.GetComponent<ParticleSystem>().Simulate(1, true, true);

            /* Destroy(effect);
             Destroy(effect2);*/
        }

    }
}
