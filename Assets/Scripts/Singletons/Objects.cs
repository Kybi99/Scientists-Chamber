using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FourGear.Singletons
{
    public class Objects : MonoBehaviour
    {
        private List<GameObject> goList;
        public static GameObject[] objects;
        public static GameObject[] otherObjects;
        public static GameObject[] framedObjects;
        public static GameObject[] levelLoader;

        public class myMonsterSorter : IComparer
        {
            int IComparer.Compare(System.Object x, System.Object y)
            {
                return ((new CaseInsensitiveComparer()).Compare(((GameObject)x).name, ((GameObject)y).name));
            }
        }
        public void Start()
        {
            IComparer myComparer = new myMonsterSorter();

            objects = GameObject.FindGameObjectsWithTag("objects");
            otherObjects = GameObject.FindGameObjectsWithTag("otherObjects");
            framedObjects = GameObject.FindGameObjectsWithTag("firstFrame");
            levelLoader = GameObject.FindGameObjectsWithTag("levelLoader");
            DestroyDuplicates(myComparer, objects);
            DestroyDuplicates(myComparer, otherObjects);
            DestroyDDOLDuplicates(myComparer, framedObjects);
            DestroyDDOLDuplicates(myComparer, levelLoader);
        }
            private void DestroyDuplicates(IComparer myComparer, GameObject[] objects)
        {
            Array.Sort(objects, myComparer);

            goList = new List<GameObject>();

            for (int i = 0; i < objects.Length; i++)
            {
                goList.Add(objects[i]);
            }
            for (int i = 0; i < goList.Count - 1; i++)
            {
                for (int j = goList.Count - 1; j > i; j--)
                {
                    if (goList[i].name + "X" == goList[j].name)
                    {
                        Destroy(goList[i]);
                        goList.RemoveAt(i);
                    }
                }
            }
        }

        private void DestroyDDOLDuplicates(IComparer myComparer, GameObject[] objects)
        {
            DestroyDuplicates(myComparer, objects);
            for (int i = 0; i < goList.Count; i++)
            {
                DontDestroyOnLoadManager.DontDestroyOnLoad(goList[i]);
            }
        }
    }
}



