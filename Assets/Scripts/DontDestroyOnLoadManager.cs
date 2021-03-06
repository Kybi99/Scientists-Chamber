using System.Collections.Generic;
using UnityEngine;

namespace FourGear
{
    public static class DontDestroyOnLoadManager
    {
        private static int counter = 0;
        private static List<GameObject> ddolObjects = new List<GameObject>();

        public static void DontDestroyOnLoad(this GameObject go)
        {
            UnityEngine.Object.DontDestroyOnLoad(go);
            ddolObjects.Add(go);
        }

        public static void ChangeNames()
        {
            foreach (GameObject go in ddolObjects)
                if (go != null)
                    foreach (Transform child in go.transform.GetComponentsInChildren<Transform>())
                    {
                        for (int i = 0; i < child.name.Length; i++)
                            if (!child.name.Contains("X"))
                                child.name += "X";
                    }

            counter++;
        }
        public static void DestroyAll()
        {
            foreach (var go in ddolObjects)
                if (go != null)
                    UnityEngine.Object.Destroy(go);

            ddolObjects.Clear();
        }
    }
}
