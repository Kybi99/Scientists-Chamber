using UnityEngine;

namespace FourGear.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Information", menuName = "Information")]
    public class Information : ScriptableObject
    {
        public Sprite blueprint;
        public string text;
        
    }
}

