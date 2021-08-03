using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;

namespace FourGear.Mechanics
{
    public class ChangeColor : MonoBehaviour
    {
        public Material material;
        [GradientUsageAttribute(true)] [SerializeField] private Gradient gradient;
        //[ColorUsageAttribute(true, true)] [SerializeField] private Color _emissionColorValue;
        //[SerializeField] private float _intensity;
        void Start()
        {
            ParticleSystem ps = GetComponent<ParticleSystem>();
            var col = ps.colorOverLifetime;
            col.enabled = true;
            col.color = gradient;
            /*Color color  = col.color as Color;
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", );*/

        }

        // Update is called once per frame
        /*void Update()
        {
            if (Particles.isParticlePlayed)
                StartCoroutine(ChangeColorAfterSecond());
        }*/
        /*public IEnumerator ChangeColorAfterSecond()
        {
            yield return new WaitForSeconds(0.5f);

            material.EnableKeyword("_EMISSION");
            material.SetVector("_EmissionColor", _emissionColorValue);


            yield return new WaitForSeconds(2);
            material.SetVector("_EmissionColor", col);

            StopCoroutine(ChangeColorAfterSecond());

        }*/
    }
}
