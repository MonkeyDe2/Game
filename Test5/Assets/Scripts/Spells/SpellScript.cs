using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{

    public Vector3 TargetPos { get; set; }
    public Vector3 TargetDir { get; set; }
    public Transform source { get; set; }
    public float damage {get; set;}
    public float Speed {get; set;}

    void Start(){
    
        TargetDir = (TargetPos - transform.position).normalized;
        StartCoroutine(Timeout());
    }

    void Update(){
        transform.position += TargetDir * Speed;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Enemy"){
            Destroy(gameObject);
        }
    }


    private IEnumerator Timeout(){
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
