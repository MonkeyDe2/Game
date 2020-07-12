using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private GameObject[] texts;

    public void ShowText(int index){
        gameObject.SetActive(true);
        texts[index].SetActive(true);
    }

    public void HideText(){
        gameObject.SetActive(false);
        foreach (GameObject gameObject in texts)
        {
            gameObject.SetActive(false);
        }
    }


}
