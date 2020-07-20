using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpell : MonoBehaviour
{
    private SpellEffect spellEffect;
    private SpellScript spellScript;
 
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Enemy"){
            spellScript = gameObject.GetComponent<SpellScript>();
            other.GetComponent<Enemy>().TakeDamage(transform.position, spellScript.damage, spellScript.source, Color.blue);
            spellEffect = other.GetComponent<SpellEffect>();
            spellEffect.Knockback(transform.position);
        }
    }
}
