using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerColor;

public class UpdatePlayerColor : MonoBehaviour
{
    public SpriteRenderer spriteRenderer1;
    public SpriteRenderer spriteRenderer2;

    void Start()
    {
        UpdateColor();
    }

    void UpdateColor()
    {
        spriteRenderer1.color = PlayerColorClass.color1[PlayerColorClass.bottomcolor];
        spriteRenderer2.color = PlayerColorClass.color2[PlayerColorClass.topcolor];
    }
}
