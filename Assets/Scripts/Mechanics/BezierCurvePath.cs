using UnityEngine;

namespace FourGear.Mechanics
{
    public class BezierCurvePath : MonoBehaviour
    {
        private Vector2 p0, p1, p2, p3;
        public ObjectMovement objectMovement;
        public Transform[] routes;



        public void Bezier(float tParam, float speedModifier)
        {
            //Bezieur formula     
            objectMovement.objectPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 3) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;
        }


        public void GetValuesForBezier(int routeNumber)
        {
            //ObjectMovement.coroutineAllowed = false;

            p0 = routes[routeNumber].GetChild(0).position;
            p1 = routes[routeNumber].GetChild(1).position;
            p2 = routes[routeNumber].GetChild(2).position;
            p3 = routes[routeNumber].GetChild(3).position;
        }
    }
}

