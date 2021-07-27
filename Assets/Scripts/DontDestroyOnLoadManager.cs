using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourGear
{
    public static class DontDestroyOnLoadManager
    {
        private static int counter = 0;
        static List<GameObject> _ddolObjects = new List<GameObject>();

        public static void DontDestroyOnLoad(this GameObject go)
        {
            UnityEngine.Object.DontDestroyOnLoad(go);
            _ddolObjects.Add(go);
        }

        public static void ChangeNames()
        {
            foreach (GameObject go in _ddolObjects)
                if (go != null)
                    foreach (Transform child in go.transform.GetComponentsInChildren<Transform>())
                    {
                        for(int i = 0; i < child.name.Length; i++)
                            if(!child.name.Contains("X"))
                                child.name += "X";
                    }
                            
            counter++;
            _ddolObjects.Clear();
        }
    }
}
