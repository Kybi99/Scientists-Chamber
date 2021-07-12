using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Transform inventoryObject;
    public static Inventory inventoryInstance;
    private int children;
    public static GameObject[] arraySlots;
    //public static GameObject[] arraySlots;
    void Start()
    {
        if (inventoryInstance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        GetSlotsFromInventory();
        /*if(arraySlots == null)
        {
               
        }  */

        inventoryInstance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    private void GetSlotsFromInventory()
    {
       
        inventoryObject = FindObjectOfType<Inventory>().transform;
        children = inventoryObject.childCount;
        arraySlots = new GameObject[inventoryObject.childCount];

        Debug.Log(inventoryObject);
        for (int i = 0; i < children; i++)
        {
            arraySlots[i] = inventoryObject.GetChild(i).gameObject;
        }
    }
}
