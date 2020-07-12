using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    private TextMeshProUGUI textDisplay;
    [SerializeField] private string[] sentences;
    [SerializeField] private Vector3 facing;
    private int index;
    private float typingSpeed = 0.05f;
    private GameObject canvas;
    private QuestGiver questGiver;
    private Animator textDisplayAnim;



    void Start()
    {   
                
    }

    IEnumerator Type(){
        textDisplay.text = "";
        foreach(char letter in sentences[index].ToCharArray()){
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void OnEnable()
    {
        textDisplayAnim = GetComponent<Animator>();
        textDisplay = GetComponent<TextMeshProUGUI>();
        canvas = GameObject.FindGameObjectWithTag("UI");
        ToggleOffCanvas();

        index = 0;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Active = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().LastMoveDir = facing;
        StartCoroutine(Type());
    }

    void Update()
    {
        if (textDisplay.text == sentences[index] && Input.anyKeyDown){
            NextSentence();
        }   
    }

    void NextSentence(){
        textDisplayAnim.SetTrigger("Change");
        if(index < sentences.Length - 1){
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        } else {
            textDisplay.text = "";       
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Active = true;
            ReceiveQuest();
            ActivateQuest();
            ToggleOnCanvas();
            this.transform.parent.gameObject.SetActive(false);

        }
    }

    void ActivateQuest(){
        if (GetComponent<QuestGiver>() != null)
        {
            GetComponent<QuestGiver>().CollectQuest();
        }
    }
    void ReceiveQuest(){
        if (GetComponent<QuestLocation>() != null)
        {
            GetComponent<QuestLocation>().HandInQuest();
        }
    }
    void ToggleOffCanvas(){
        canvas.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 0;
        canvas.transform.GetChild(1).GetComponent<CanvasGroup>().alpha = 0;
    }
    void ToggleOnCanvas(){
        canvas.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 1;
        canvas.transform.GetChild(1).GetComponent<CanvasGroup>().alpha = 1;
    }
}
