using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpell : MonoBehaviour
{
    public Vector3 TargetPos { get; set; }
    public Vector3 TargetDir { get; set; }
    public Transform source { get; set; }
    public float damage {get; set;}
    public float Speed {get; set;}

    void Start(){
    
        TargetDir = (TargetPos - transform.position).normalized;
    }

    void Update(){
        transform.position += TargetDir * Speed;
        
        float distance = Vector2.Distance(TargetPos, transform.position);
        if(distance <= 0.5f){
            LightSpellP2 lightSpellP2 = Instantiate(GameAssets.i.boom, transform.position, Quaternion.identity).GetComponent<LightSpellP2>();
            lightSpellP2.damage = damage;
            lightSpellP2.source = source;
            Destroy(gameObject);
        }
    }

}
