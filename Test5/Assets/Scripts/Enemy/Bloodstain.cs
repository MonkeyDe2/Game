using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodstain : MonoBehaviour
{
    
    
    void Start()
    {
     transform.Rotate(0,0,Random.Range(0,360));
     transform.localScale = new Vector3(Random.Range(10,30),Random.Range(10,30),0);


    }

}
