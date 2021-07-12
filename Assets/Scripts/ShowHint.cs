using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHint : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;
    // Start is called before the first frame update
    public void ShowHints()
    {
        if(Canvas.activeSelf == false)
            Canvas.SetActive(true);
        else 
            Canvas.SetActive(false);
    }
}
