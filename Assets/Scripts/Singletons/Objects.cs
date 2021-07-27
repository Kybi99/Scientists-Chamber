using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FourGear.Singletons
{
    public class Objects : MonoBehaviour
    {
        List<GameObject> goList;
        private GameObject[] objects;
        private GameObject[] otherObjects;
        private GameObject[] framedObjects;

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
            DestroyDuplicates(myComparer, objects);
            DestroyDuplicates(myComparer, otherObjects);
            DestroyDDOLDuplicates(myComparer, framedObjects);
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
                    if (goList[i].name + "X" == goList[j].name )
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



