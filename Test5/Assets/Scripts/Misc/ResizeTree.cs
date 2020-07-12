using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeTree : MonoBehaviour
{
    Vector3 actualScale;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player"){
            actualScale =  transform.parent.transform.localScale;
        Vector3 addScale = actualScale + new Vector3(5,7,0);
        Vector3 minusScale = actualScale + new Vector3(-5,-7,0);

        transform.parent.transform.localScale = Vector3.Lerp(actualScale, addScale, 1 * Time.deltaTime);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player"){
        actualScale =  transform.parent.transform.localScale;
        Vector3 addScale = actualScale + new Vector3(2,4,0);
        Vector3 minusScale = actualScale + new Vector3(-2,-4,0);

        transform.parent.transform.localScale = Vector3.Lerp(actualScale, minusScale, 1 * Time.deltaTime);
        }
    }
}
