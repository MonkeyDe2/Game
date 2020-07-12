using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIcon : MonoBehaviour
{

    
    private SpriteRenderer spriteRenderer;
    private Color invis = Color.white;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        invis.a = 0f;
    }

    
    public void UpdateIcon(int index){
        if (index == 0)
        {
            spriteRenderer.color = Color.yellow;
            spriteRenderer.sortingOrder = 10;
        } else if (index == 1){
            spriteRenderer.color =  Color.blue;
            spriteRenderer.sortingOrder = 5;
        } else {
            spriteRenderer.color = invis;
        }
        
    }
}
