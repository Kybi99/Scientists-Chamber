using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
        /*if(x==0)
        {
            infoImage.sprite = information1.blueprint;    
            infoText.text = information1.text;
        }  
        else if(x==1)
        {
            infoImage.sprite = information2.blueprint;    
            infoText.text = information2.text;
        }*/
    }
}
