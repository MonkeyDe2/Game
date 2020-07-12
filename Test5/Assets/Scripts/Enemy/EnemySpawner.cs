using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class EnemySpawner : MonoBehaviour
{

  [SerializeField] private Transform playerTransform;

  [SerializeField] private Enemy enemy;


    // void Start(){
    //   enemy = gameObject.GetComponent<Enemy>();
    //   playerTransform = gameobject.GetComponent<Transform>();
    // }

    void Update()
    {
      if(Input.GetKeyDown(KeyCode.M)){
        enemy.Create(playerTransform.position + UtilClass.GetRandomDir() * Random.Range(100f, 200f));
      }

    }
}
