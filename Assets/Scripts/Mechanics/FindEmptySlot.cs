using UnityEngine;

namespace FourGear.Mechanics
{
    public class FindEmptySlot : MonoBehaviour
    {
        public int CheckFirstEmptySlot(GameObject[] arraySlots)
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
    }
}
