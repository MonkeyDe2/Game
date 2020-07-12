using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomizeSize : MonoBehaviour
{
    [SerializeField] private bool BIGTREE;

    void Start()
    {

        if (BIGTREE){
             transform.localScale = new Vector3(2f,2f,1);
        } else {
            float randomx = Random.Range(0.7f, 1.5f);
            float randomy = Random.Range(0.7f, 1.5f);

            transform.localScale = new Vector3(randomx,randomy,1);

            Color color = new Color(Random.Range(0.7f,1f),Random.Range(0.7f,1f),Random.Range(0.7f,1f));

            GetComponent<SpriteRenderer>().color =  color;

            gameObject.transform.parent.GetComponent<TilemapRenderer>().enabled = false;
            gameObject.transform.parent.GetComponent<TilemapCollider2D>().enabled = false;
        }

        


    }
    
}
