using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1 : MonoBehaviour
{
    public Sprite triangle;
    public Sprite diamond; 
    
    //Can possible do [SerializeField] private Sprite[] shapes
    //This makes it so you can have an array of different shapes (say 10) to choose from, could be useful for larger scale
    //Private to make it inaccessable by other scripts and don't show up on inspector, great to keep coding 'safe' but not important in this case
    //I like to use private variables whereever posible so its enater.
    //Note: public variables show up in inspector unless you do [NonSerialized]/[HideInInspector] which makes inspector "cleaner" 

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
//This is good! works perfectly. Although the else statement will never be ran, therefore unnecessary. Can think about how to change from one image to another
//but works fine :) Well Done! 
}
