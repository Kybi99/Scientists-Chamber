using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace FourGear.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Information", menuName = "Information")]
    public class Information : ScriptableObject
    {
        public Sprite blueprint;
        //[SerializeField] Text Info; 
        public string text;
    }
}

