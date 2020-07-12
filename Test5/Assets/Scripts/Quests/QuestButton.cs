using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    
    private Text nameText;
    private Text descriptionText;
    public QuestEvent thisEvent;
    QuestEvent.EventStatus status;

    void Start()
    {
        
    }
    public void Setup(QuestEvent e, GameObject QuestLog){
        thisEvent = e;
        transform.SetParent(QuestLog.transform);
        nameText = transform.GetChild(0).GetComponent<Text>();
        descriptionText = transform.GetChild(1).GetComponent<Text>();
        nameText.text = thisEvent.name;
        descriptionText.text = thisEvent.description;
    }

    public void DestroyButton()
    {
        Destroy(gameObject);
    }
}
