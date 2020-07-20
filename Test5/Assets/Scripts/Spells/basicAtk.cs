﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicAtk : MonoBehaviour
{
    private SpellEffect spellEffect;
    private SpellScript spellScript;
 
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Enemy"){
            spellScript = gameObject.GetComponent<SpellScript>();
            other.GetComponent<Enemy>().TakeDamage(transform.position, spellScript.damage, spellScript.source, Color.white);
        }
    }
}