using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FourGear.Singletons;
using FourGear.UI;

namespace FourGear.Mechanics
{
    public class ObjectPath : MonoBehaviour
    {
        [SerializeField] private Transform[] routes;
        //[SerializeField] private GameObject[] correctForms; 
        private static int routeToGo;
        private int routeTaken;
        //private int numberOfSlots;
        private float tParam;
        private float speedModifier;
        private string sortingLayer;
        private Vector2 objectPosition;
        private Vector2 startPosition;
        private Transform resetParent;
        public static bool coroutineAllowed;
        [HideInInspector] public bool inInventory;
        private SpriteRenderer sprite;
        //public static Transform inventory; 

        void Start()
        {
            routeToGo = 0;
            tParam = 0f;
            speedModifier = 2f;
            coroutineAllowed = true;
            inInventory = false;
            resetParent = this.transform.parent;
            sprite = this.gameObject.GetComponent<SpriteRenderer>();
            sortingLayer = sprite.sortingLayerName;

        }

        private IEnumerator GoByTheRoute(int routeNumber)
        {
            coroutineAllowed = false;
            tParam = 0f;
            //Debug.Log(routeNumber);
            Vector2 p0 = routes[routeNumber].GetChild(0).position;                                                                              //get routes
            Vector2 p1 = routes[routeNumber].GetChild(1).position;
            Vector2 p2 = routes[routeNumber].GetChild(2).position;
            Vector2 p3 = routes[routeNumber].GetChild(3).position;

            routeTaken = routeToGo;
            startPosition = new Vector2(transform.position.x, transform.position.y);
            sprite.sortingLayerName = "Prvi plan";

            while (tParam < 1)
            {
                tParam += Time.deltaTime * speedModifier;
                objectPosition = Mathf.Pow(1 - tParam, 3) * p0 +                                                                            //Bezieur formula     
                    3 * Mathf.Pow(1 - tParam, 3) * tParam * p1 +
                    3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                    Mathf.Pow(tParam, 3) * p3;

                //Update position
                transform.position = objectPosition;
                transform.Rotate(new Vector3(0, 0, -360 * Time.deltaTime));
                yield return new WaitForEndOfFrame();
            }


            this.transform.parent = Inventory.arraySlots[routeTaken].transform;                                                                        //Fix it in slot 
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector2(0.25f, 0.25f);
            this.transform.position = new Vector2(Inventory.arraySlots[routeTaken].transform.position.x, Inventory.arraySlots[routeTaken].transform.position.y);

            StopCoroutine(GoByTheRoute(routeToGo));

            routeToGo++;
            inInventory = true;
            //yield return new WaitForSeconds(0.25f);
            coroutineAllowed = true;
        }

        private IEnumerator GoByTheRoute2(int routeNumber)
        {
            coroutineAllowed = false;
            tParam = 1f;

            Vector2 p0 = routes[routeNumber].GetChild(0).position;
            Vector2 p1 = routes[routeNumber].GetChild(1).position;
            Vector2 p2 = routes[routeNumber].GetChild(2).position;
            Vector2 p3 = routes[routeNumber].GetChild(3).position;

            while (tParam > 0)
            {
                tParam -= Time.deltaTime * speedModifier;
                objectPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                    3 * Mathf.Pow(1 - tParam, 3) * tParam * p1 +
                    3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                    Mathf.Pow(tParam, 3) * p3;

                transform.position = objectPosition;
                transform.Rotate(new Vector3(0, 0, 360 * Time.deltaTime));
                yield return new WaitForEndOfFrame();
            }

            sprite.sortingLayerName = sortingLayer;
            this.transform.parent = resetParent;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector2(0.5f, 0.5f);
            this.transform.position = startPosition;

            StopCoroutine(GoByTheRoute2(routeTaken));

            inInventory = false;
            // yield return new WaitForSeconds(0.25f);
            coroutineAllowed = true;
        }

        int CheckFirstEmptySlot(GameObject[] arraySlots)
        {
            int id = -1;
            for (int i = 0; i < arraySlots.Length; i++)
            {
                if (arraySlots[i] != null && arraySlots[i].transform.childCount == 0)
                {
                    id = i;
                    break;
                }
            }

            return id;
        }
        public void OnMouseDown()                                                                                                           //OnClickFuntions
        {

            if (Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name == "Skladiste")
            {

                if (!inInventory && coroutineAllowed && routeToGo < Inventory.arraySlots.Length && ShowHint.canClick)
                {
                    routeToGo = CheckFirstEmptySlot(Inventory.arraySlots);
                    if (routeToGo != -1)
                        StartCoroutine(GoByTheRoute(routeToGo));
                }

                else if (inInventory && coroutineAllowed && ShowHint.canClick)
                {
                    routeToGo = CheckFirstEmptySlot(Inventory.arraySlots);
                    StartCoroutine(GoByTheRoute2(routeTaken));
                }
            }
        }
    }
}
