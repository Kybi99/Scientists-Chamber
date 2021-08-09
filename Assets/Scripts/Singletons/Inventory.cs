using UnityEngine;

namespace FourGear.Singletons
{
    public class Inventory : MonoBehaviour
    {
        private static Transform inventoryObject;
        private int children;
        public static Inventory inventoryInstance;
        public static GameObject[] arraySlots;
        void Start()
        {
            if (inventoryInstance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            GetSlotsFromInventory();

            inventoryInstance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }

        private void GetSlotsFromInventory()
        {
            inventoryObject = FindObjectOfType<Inventory>().transform;
            children = inventoryObject.childCount;
            arraySlots = new GameObject[inventoryObject.childCount];

            for (int i = 0; i < children; i++)
            {
                arraySlots[i] = inventoryObject.GetChild(i).gameObject;
            }
        }
    }
}