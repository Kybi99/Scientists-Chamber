using UnityEngine;

namespace FourGear
{
    public class PlaceholdersDDOL : MonoBehaviour
    {
        public static PlaceholdersDDOL placeholdersDDOL;
        void Awake()
        {
            if(placeholdersDDOL != null)
            {
                Destroy(this.gameObject);
                return;
            }

            placeholdersDDOL = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }
}
