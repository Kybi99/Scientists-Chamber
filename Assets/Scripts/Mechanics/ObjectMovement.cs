using System.Collections;
using UnityEngine;
using FourGear.Singletons;

namespace FourGear.Mechanics
{
    public class ObjectMovement : MonoBehaviour
    {
        [SerializeField] private BezierCurvePath bezierCurvePath;
        private float tParam;
        private float speedModifier;
        private string sortingLayer;
        private Vector3 startPosition;
        private Transform resetParent;
        private SpriteRenderer sprite;
        private Quaternion resetRotation;
        private Vector2 resetCollider;
        private Vector2 resetOffset;
        private BoxCollider2D boxCollider2D;
        public static int routeToGo;
        public static bool coroutineAllowed;
        [HideInInspector] public bool inInventory;
        [HideInInspector] public int routeTaken;
        [HideInInspector] public Vector2 objectPosition;




        void Start()
        {
            routeToGo = 0;
            tParam = 0f;
            speedModifier = 0.8f;
            coroutineAllowed = true;
            inInventory = false;
            resetParent = this.transform.parent;
            sprite = this.gameObject.GetComponent<SpriteRenderer>();
            sortingLayer = sprite.sortingLayerName;
            resetRotation = this.gameObject.transform.rotation;
            boxCollider2D = this.gameObject.GetComponent<BoxCollider2D>();
            resetOffset = boxCollider2D.offset;
            resetCollider = boxCollider2D.size;
        }

        public IEnumerator GoByTheRoute(int routeNumber)
        {
            tParam = 0f;
            bezierCurvePath.GetValuesForBezier(routeNumber);

            routeTaken = routeToGo;
            startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            sprite.sortingLayerName = "Ispred svega";

            while (tParam < 1)
            {
                tParam += Time.deltaTime * speedModifier;
                bezierCurvePath.Bezier(tParam, speedModifier);
                //Update position and rotation
                transform.position = objectPosition;
                // transform.Rotate(new Vector3(0, 0, -360 * Time.deltaTime * speedModifier));

                yield return new WaitForEndOfFrame();
            }
            Particles.PauseParticles();

            AddXtoName();

            FixItInSlot();

            Particles.ResumeParticles();

            StopCoroutine(GoByTheRoute(routeToGo));

            routeToGo++;
            inInventory = true;
            yield return new WaitForSeconds(0.4f);
            Particles.RestartParticles();
            coroutineAllowed = true;
        }

        private void AddXtoName()
        {
            for (int i = 0; i < this.gameObject.name.Length; i++)
                if (!this.gameObject.name.Contains("X"))
                    this.gameObject.name += "X";
        }

        private void FixItInSlot()
        {
            if (this.gameObject.name.Contains("Y"))
                transform.localScale = new Vector2(0.4f, 0.4f);
            else if (this.gameObject.name.Contains("W"))
                transform.localScale = new Vector2(0.5f, 0.5f);
            else
                transform.localScale = new Vector2(0.7f, 0.7f);
            transform.rotation = Quaternion.Euler(Vector3.zero);
            this.transform.parent = Inventory.arraySlots[routeTaken].transform;
            this.transform.position = new Vector3(Inventory.arraySlots[routeTaken].transform.position.x, Inventory.arraySlots[routeTaken].transform.position.y, -3f);
            this.boxCollider2D.size = this.gameObject.transform.parent.GetComponent<BoxCollider2D>().size * 2.5f;
            boxCollider2D.offset = Vector2.zero;
        }

        public IEnumerator GoByTheRoute2(int routeNumber)
        {
            tParam = 1f;
            bezierCurvePath.GetValuesForBezier(routeNumber);

            transform.localScale = new Vector2(1, 1);

            while (tParam > 0)
            {
                tParam -= Time.deltaTime * speedModifier;
                bezierCurvePath.Bezier(tParam, speedModifier);
                transform.position = objectPosition;
                // transform.Rotate(new Vector3(0, 0, 360 * Time.deltaTime * speedModifier));

                yield return new WaitForEndOfFrame();
            }

            Particles.PauseParticles();

            PutItBack();

            Particles.ResumeParticles();

            StopCoroutine(GoByTheRoute2(routeTaken));

            inInventory = false;

            yield return new WaitForSeconds(0.4f);
            Particles.RestartParticles();
            coroutineAllowed = true;
        }

        private void PutItBack()
        {
            transform.rotation = resetRotation;
            sprite.sortingLayerName = sortingLayer;
            this.transform.parent = resetParent;
            this.transform.position = startPosition;
            boxCollider2D.size = resetCollider;
            boxCollider2D.offset = resetOffset;
            transform.localScale = new Vector2(1, 1);

        }
    }
}
