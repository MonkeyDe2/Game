using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetColor : MonoBehaviour
{
    private Image image;
    [SerializeField] private ChangingColor changingColor; 
    [SerializeField] private int index;
    public bool color1;

    void Start()
    {
        image = GetComponent<Image>();

        if (color1){
            image.color = changingColor.color1[index];
        } else{
            image.color = changingColor.color2[index];
        }
       
    }
}
