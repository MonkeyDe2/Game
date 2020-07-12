using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorting : MonoBehaviour
{
    private int sortingOrderBase = 5000;
    [SerializeField] private int offset = 0;

    [SerializeField] private bool runOnlyOnce = false;
    [SerializeField] private int plus = 0;

    private float timer;
    private float timerMax = .1f;
    private Renderer myRenderer;

    void Awake()
    {
        myRenderer = gameObject.GetComponent<Renderer>();

    }

    void LateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            myRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y - offset) + plus;
        }
        if (runOnlyOnce){
            Destroy(this);
        }
    }
}
