using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace FourGear.ScriptableObjects
{
    public class InfoDisplay : MonoBehaviour
    {
        [SerializeField] Information[] information;
        [SerializeField] Image infoImage;
        [SerializeField] TMP_Text infoText;

        public int WriteCorrectDataOnCanvas(int index)
        {
            infoImage.sprite = information[index].blueprint;
            infoText.text = information[index].text;
 
            return index;
        }
    }

}
