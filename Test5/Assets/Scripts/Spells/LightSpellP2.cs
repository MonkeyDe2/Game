using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpellP2 : MonoBehaviour
{

    private static List<Collider2D> enemyList;
    private Vector3 scaleChange = new Vector3(0.5f, 0.5f, 0f);
    public float damage {get; set;}
    public float Speed {get; set;}
    public Transform source { get; set; }

    private void Start() {
        StartCoroutine(Timeout());
        enemyList = new List<Collider2D>();
    }

    private void Update() {
        transform.localScale += scaleChange;
    }

    void OnTriggerEnter2D(Collider2D other){
        
        bool contains = enemyList.Exists(o => o == other);
        
        if(other.tag == "Enemy" && contains == false){
            other.GetComponent<Enemy>().TakeDamage(transform.position, damage, source);
            enemyList.Add(other);
        }
    }

    private IEnumerator Timeout(){
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
