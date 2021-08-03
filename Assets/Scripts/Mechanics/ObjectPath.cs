using System.Collections;
using UnityEngine;
using FourGear.Singletons;

namespace FourGear.Mechanics
{
    public class ObjectPath : MonoBehaviour
    {
        [SerializeField] private BezierMovement bezierMovement;
        public static int routeToGo;
        [HideInInspector] public int routeTaken;
        private float tParam;
        private float speedModifier;
        private string sortingLayer;
        private Vector3 startPosition;
        private Vector2 p0, p1, p2, p3;
        [HideInInspector] public Vector2 objectPosition;
        private Transform resetParent;
        private SpriteRenderer sprite;
        public static bool coroutineAllowed;
        [HideInInspector] public bool inInventory;
        private Quaternion resetRotation;
        private Vector2 resetCollider;
        private BoxCollider2D boxCollider2D;


        void Start()
        {
            routeToGo = 0;
            tParam = 0f;
            speedModifier = 0.8f;
            coroutineAllowed = true;
            inInventory = false;
            resetParent = this.transform.parent;
            //bezierMovement.InstantiateRoutes(this.gameObject);
            sprite = this.gameObject.GetComponent<SpriteRenderer>();
            sortingLayer = sprite.sortingLayerName;
            resetRotation = this.gameObject.transform.rotation;
            boxCollider2D = this.gameObject.GetComponent<BoxCollider2D>();
            resetCollider = boxCollider2D.size;
        }

        public IEnumerator GoByTheRoute(int routeNumber)
        {
            //bezierMovement.ObjectParentConfig();

            tParam = 0f;
            bezierMovement.GetValuesForBezier(routeNumber);

            routeTaken = routeToGo;
            startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            sprite.sortingLayerName = "Ispred svega";

            while (tParam < 1)
            {
                tParam += Time.deltaTime * speedModifier;
                bezierMovement.Bezier(tParam, speedModifier);
                //Update position and rotation
                transform.position = objectPosition;
               // transform.Rotate(new Vector3(0, 0, -360 * Time.deltaTime * speedModifier));

                yield return new WaitForEndOfFrame();
            }

            AddXtoName();

            FixItInSlot();

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
            transform.localScale = new Vector2(0.5f, 0.5f);
            transform.rotation = Quaternion.Euler(Vector3.zero);
            this.transform.parent = Inventory.arraySlots[routeTaken].transform;                                                                        //Fix it in slot 
            this.transform.position = new Vector2(Inventory.arraySlots[routeTaken].transform.position.x, Inventory.arraySlots[routeTaken].transform.position.y);
            this.boxCollider2D.size = this.gameObject.transform.parent.GetComponent<BoxCollider2D>().size * 2.5f;
        }

        public IEnumerator GoByTheRoute2(int routeNumber)
        {
            // bezierMovement.ObjectParentConfig();

            tParam = 1f;
            bezierMovement.GetValuesForBezier(routeNumber);

            while (tParam > 0)
            {
                tParam -= Time.deltaTime * speedModifier;
                bezierMovement.Bezier(tParam, speedModifier);
                transform.position = objectPosition;
               // transform.Rotate(new Vector3(0, 0, 360 * Time.deltaTime * speedModifier));

                yield return new WaitForEndOfFrame();
            }

            PutItBack();

            StopCoroutine(GoByTheRoute2(routeTaken));

            inInventory = false;

            yield return new WaitForSeconds(0.4f);
            Particles.RestartParticles();
            coroutineAllowed = true;
        }

        private void PutItBack()
        {
            transform.localScale = new Vector2(1, 1);
            transform.rotation = resetRotation;
            sprite.sortingLayerName = sortingLayer;
            this.transform.parent = resetParent;
            this.transform.position = startPosition;
            boxCollider2D.size = resetCollider;
        }
    }
}
