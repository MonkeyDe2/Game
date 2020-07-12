using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : MonoBehaviour
{
    private SpellEffect spellEffect;
 
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Enemy"){
            spellEffect = other.GetComponent<SpellEffect>();
            spellEffect.StartBurn("Burn");
        }
    }

}
