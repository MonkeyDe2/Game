using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBookContainer : MonoBehaviour
{
    [SerializeField] private GameObject[] spellIcons;
    [SerializeField] private Image[] spellHotbar;
    public int[] spellIndexInHotbar = new int[3];

    private CanvasGroup canvasGroup;

    private int selected = -1;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (canvasGroup.alpha == 0) return;

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            selected = 0;
            //show indicator
            //check if others have that spell            
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            selected = 1;
            //show indicator
            //check if others have that spell
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            selected = 2;
            //show indicator
            //check if others have that spell
            
        }
    }

    public void Selected(int i){
        //check which spell hotbar is selected
        if (selected >= 0){
           spellHotbar[selected].GetComponent<Image>().sprite = spellIcons[i].GetComponent<Image>().sprite;
           spellIndexInHotbar[selected] = i;
           selected = -1; 
        }
        
    }
}
