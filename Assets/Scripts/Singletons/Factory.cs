/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System;

namespace FourGear.Singletons
{
   
    public abstract class Factory : MonoBehaviour
    {
        private void Start()
        {
            
        }
        public abstract string Name { get; }
        public abstract void Process();
    }
    
    /*public class Object1 : Factory
    {
        private static Factory objectInstance;
        public override string Name => "Object1";

        public override void Process()
        {
            objectInstance = Factorization.GetFactory("Object1");
            
            if (objectInstance != null)
            {
                Destroy(objectInstance);
                return;
            }

            objectInstance = this;

        }

    }
    public static class Factorization
    {
        private static Dictionary<string, Type> objectsByName;
        private static bool isInitialized => objectsByName != null;

        private static void InitializeFactory()
        {
            if(isInitialized)
                return;

            var objectTypes = Assembly.GetAssembly(typeof(Factory)).GetTypes().Where(myType => myType.IsClass &&  !myType.IsAbstract && myType.IsSubclassOf(typeof(Factory)));

            objectsByName = new Dictionary<string, Type>();

            foreach (var type in objectTypes)
            {   
                var tempEffect = Activator.CreateInstance(type) as Factory;
                objectsByName.Add(tempEffect.Name, type);
            }
        }
        public static Factory GetFactory(string objectType)
        {
            InitializeFactory();

            if(objectsByName.ContainsKey(objectType))
            {
                Type type = objectsByName[objectType];
                var objectX = Activator.CreateInstance(type) as Factory;
                return objectX;
            }

            return null;
        }

        internal static IEnumerable<string> GetObjectName()
        {
            UnityEngine.Debug.Log("test");
            InitializeFactory();
            return objectsByName.Keys;
        }
    }
}
*/