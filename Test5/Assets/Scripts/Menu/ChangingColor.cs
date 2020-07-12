using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerColor;

public class ChangingColor : MonoBehaviour
{
     public Color[] color1;
     public Color[] color2;
    public SpriteRenderer bottom;
    public SpriteRenderer top;
    

    public int colorIndex;
    public Player player;
    public SpellBook spellBook;
    public Animator[] animator;

    void Awake()
    {
        spellBook.enabled = false;
        player.enabled = false;

        foreach (Animator an in animator){
            an.enabled = false;
        }

        PlayerColorClass.color1 = color1;
        PlayerColorClass.color2 = color2;
        
    }

    void Start()
    {
        bottom.color = color1[0];
        top.color = color2[0];
    }

    public void ChangeBottomColor(int index){
        bottom.color = color1[index];
        PlayerColorClass.bottomcolor = index;
       
                
        
    }
    public void ChangeTopColor(int index){
        top.color = color2[index];
        PlayerColorClass.topcolor = index;
        
        
    }

}

