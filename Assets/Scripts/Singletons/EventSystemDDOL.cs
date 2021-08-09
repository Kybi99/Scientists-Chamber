using UnityEngine;

namespace FourGear
{
    public class EventSystemDDOL : MonoBehaviour
    {
        public static EventSystemDDOL eventSystemDDOL;
        void Start()
        {
            if (eventSystemDDOL != null)
            {
                Destroy(this.gameObject);
                return;
            }

            eventSystemDDOL = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }
}
