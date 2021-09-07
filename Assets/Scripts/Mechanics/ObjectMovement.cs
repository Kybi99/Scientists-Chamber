using System.Collections;
using UnityEngine;
using FourGear.Singletons;

namespace FourGear.Mechanics
{
    public class ObjectMovement : MonoBehaviour
    {
        [SerializeField] private BezierCurvePath bezierCurvePath;
        public bool thisObjectIsFlying;
        public static bool isNextSceneAllowed;
        private float tParam;
        private float speedModifier;
        private string sortingLayer;
        private Vector3 startPosition;
        private Transform resetParent;
        private SpriteRenderer sprite;
        private Quaternion resetRotation;
        private Vector2 resetCollider;
        private Vector3 resetScale;
        private Vector2 resetOffset;
        private BoxCollider2D boxCollider2D;
        public static int numberOfObjectsFlying;
        public static int routeToGo;
        public static bool coroutineAllowed;
        public Particles particles;
        [HideInInspector] public bool inInventory;
        [HideInInspector] public int routeTaken;
        [HideInInspector] public Vector2 objectPosition;

        void Start()
        {
            numberOfObjectsFlying = 0;
            routeToGo = 0;
            tParam = 0f;
            speedModifier = 0.8f;
            coroutineAllowed = true;
            inInventory = false;
            isNextSceneAllowed = true;
            thisObjectIsFlying = false;
            resetParent = this.transform.parent;
            sprite = this.gameObject.GetComponent<SpriteRenderer>();
            sortingLayer = sprite.sortingLayerName;
            resetRotation = this.gameObject.transform.rotation;
            boxCollider2D = this.gameObject.GetComponent<BoxCollider2D>();
            resetOffset = boxCollider2D.offset;
            resetCollider = boxCollider2D.size;
            resetScale = this.gameObject.transform.localScale;
        }

        public IEnumerator GoByTheRoute(int routeNumber)
        {
            isNextSceneAllowed = false;

            if (!thisObjectIsFlying && coroutineAllowed)
            {
                thisObjectIsFlying = true;
                numberOfObjectsFlying++;
                //Debug.Log(numberOfObjectsFlying);
                tParam = 0f;
                bezierCurvePath.GetValuesForBezier(routeNumber);


                routeTaken = routeToGo;
                this.transform.SetParent(Inventory.arraySlots[routeTaken].transform);

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
                particles.PauseParticles();

                AddXtoName();

                FixItInSlot();

                particles.ResumeParticles();

                StopCoroutine(GoByTheRoute(routeToGo));

                //routeToGo++;
                inInventory = true;

                yield return new WaitForSeconds(0.5f);

                foreach (Transform child in transform)
                    Destroy(child.gameObject);
                //particles.RestartParticles();

                yield return new WaitForSeconds(0.5f);

                numberOfObjectsFlying--;

                if (numberOfObjectsFlying == 0)
                    isNextSceneAllowed = true;
        

                coroutineAllowed = true;

                thisObjectIsFlying = false;
            }

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

            this.transform.position = new Vector3(Inventory.arraySlots[routeTaken].transform.position.x, Inventory.arraySlots[routeTaken].transform.position.y, -3f);
            this.boxCollider2D.size = this.gameObject.transform.parent.GetComponent<BoxCollider2D>().size * 2.5f;
            boxCollider2D.offset = Vector2.zero;
        }

        public IEnumerator GoByTheRoute2(int routeNumber)
        {
            isNextSceneAllowed = false;

            if (!thisObjectIsFlying && coroutineAllowed)
            {
                thisObjectIsFlying = true;
                numberOfObjectsFlying++;
                //Debug.Log(numberOfObjectsFlying);
                tParam = 1f;
                bezierCurvePath.GetValuesForBezier(routeNumber);

                transform.localScale = resetScale;

                while (tParam > 0)
                {
                    tParam -= Time.deltaTime * speedModifier;
                    bezierCurvePath.Bezier(tParam, speedModifier);
                    transform.position = objectPosition;
                    // transform.Rotate(new Vector3(0, 0, 360 * Time.deltaTime * speedModifier));

                    yield return new WaitForEndOfFrame();
                }

                particles.PauseParticles();

                PutItBack();

                particles.ResumeParticles();

                StopCoroutine(GoByTheRoute2(routeTaken));

                inInventory = false;

                yield return new WaitForSeconds(0.5f);
                foreach (Transform child in transform)
                    Destroy(child.gameObject);
                yield return new WaitForSeconds(0.5f);
                //particles.RestartParticles();
                numberOfObjectsFlying--;

                if (numberOfObjectsFlying == 0)
                    isNextSceneAllowed = true;


                coroutineAllowed = true;

                thisObjectIsFlying = false;

            }

        }

        private void PutItBack()
        {
            transform.rotation = resetRotation;
            sprite.sortingLayerName = sortingLayer;
            this.transform.parent = resetParent;
            this.transform.position = startPosition;
            boxCollider2D.size = resetCollider;
            boxCollider2D.offset = resetOffset;
        }
    }
}
