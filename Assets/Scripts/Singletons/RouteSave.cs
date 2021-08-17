using UnityEngine;

namespace FourGear.Singletons
{
    public class RouteSave : MonoBehaviour
    {
        public static RouteSave routeInstance;
        void Start()
        {
            routeInstance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }
}

