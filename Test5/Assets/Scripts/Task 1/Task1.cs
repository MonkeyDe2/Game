using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1 : MonoBehaviour
{
    public Sprite triangle;
    public Sprite diamond;

    private SpriteRenderer spriteR;

    // Start is called before the first frame update
    private void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = diamond;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = triangle;
        }

    }

}
