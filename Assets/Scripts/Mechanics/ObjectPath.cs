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
        private Vector2 startPosition;
        private Vector2 p0, p1, p2, p3;
        [HideInInspector] public Vector2 objectPosition;
        private Transform resetParent;
        private SpriteRenderer sprite;
        public static bool coroutineAllowed;
        [HideInInspector] public bool inInventory;

        void Start()
        {
            routeToGo = 0;
            tParam = 0f;
            speedModifier = 1.5f;
            coroutineAllowed = true;
            inInventory = false;
            resetParent = this.transform.parent;
            //bezierMovement.InstantiateRoutes(this.gameObject);
            sprite = this.gameObject.GetComponent<SpriteRenderer>();
            sortingLayer = sprite.sortingLayerName;
        }

        public IEnumerator GoByTheRoute(int routeNumber)
        {
            //bezierMovement.ObjectParentConfig();

            tParam = 0f;
            bezierMovement.GetValuesForBezier(routeNumber);

            routeTaken = routeToGo;
            startPosition = new Vector2(transform.position.x, transform.position.y);
            sprite.sortingLayerName = "Objekti";

            while (tParam < 1)
            {
                tParam += Time.deltaTime * speedModifier;
                bezierMovement.Bezier(tParam, speedModifier);
                //Update position and rotation
                transform.position = objectPosition;
                transform.Rotate(new Vector3(0, 0, -360 * Time.deltaTime * speedModifier));

                yield return new WaitForEndOfFrame();
            }

            AddXtoName();

            FixItInSlot();

            StopCoroutine(GoByTheRoute(routeToGo));

            routeToGo++;
            inInventory = true;
            coroutineAllowed = true;
            //yield return new WaitForSeconds(0.2f);
            Particles.RestartParticles();
        }

        private void AddXtoName()
        {
            for (int i = 0; i < this.gameObject.name.Length; i++)
                if (!this.gameObject.name.Contains("X"))
                    this.gameObject.name += "X";
        }

        private void FixItInSlot()
        {
            transform.localScale = new Vector2(0.85f, 0.85f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            this.transform.parent = Inventory.arraySlots[routeTaken].transform;                                                                        //Fix it in slot 
            this.transform.position = new Vector2(Inventory.arraySlots[routeTaken].transform.position.x, Inventory.arraySlots[routeTaken].transform.position.y);
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
                transform.Rotate(new Vector3(0, 0, 360 * Time.deltaTime * speedModifier));

                yield return new WaitForEndOfFrame();
            }

            PutItBack();

            StopCoroutine(GoByTheRoute2(routeTaken));

            inInventory = false;
            coroutineAllowed = true;

            Particles.RestartParticles();
        }

        private void PutItBack()
        {
            transform.localScale = new Vector2(1, 1);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            sprite.sortingLayerName = sortingLayer;
            this.transform.parent = resetParent;
            this.transform.position = startPosition;
        }
    }
}
