using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLocation : MonoBehaviour
{
    private QuestManager qManager;
    public QuestEvent qEvent;
    private Transform questLog;


    void Start()
    {
        qManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<QuestManager>();

    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag != "Player") return;
        HandInQuest();
        
        
    }

    public void Setup(QuestManager qm, QuestEvent qe)
    {
        qManager = qm;
        qEvent = qe;
    }

    public void HandInQuest(){
        if (qEvent.status != QuestEvent.EventStatus.CURRENT) return;

        qEvent.UpdateQuestEvent(QuestEvent.EventStatus.DONE);
        qManager.UpdateQuestsOnCompletion(qEvent);

        questLog = GameObject.FindGameObjectWithTag("QuestLog").transform;

        foreach (Transform child in questLog)
        {
            QuestButton questButtonScript = child.GetComponent<QuestButton>();
            if (questButtonScript.thisEvent.name == qEvent.name)
            {
                questButtonScript.DestroyButton();
                return;
            }
        }

    }
}
