using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf(){
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
