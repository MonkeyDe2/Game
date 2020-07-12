using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    private QuestManager questManager;
    public QuestEvent questEvent;
    private GameObject questLog;
    [SerializeField] private bool defualtAvailable;


        void Start()
    {
        questManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<QuestManager>();      
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player") return;
        CollectQuest();
        
    }

    public void Setup(QuestManager qm, QuestEvent qe)
    {
        questManager = qm;
        questEvent = qe;

        if (defualtAvailable){
            questEvent.UpdateQuestEvent(QuestEvent.EventStatus.NOTTAKEN);
        }

    }

    public void CollectQuest(){
        questLog = GameObject.FindGameObjectWithTag("QuestLog");
        if (questEvent.status != QuestEvent.EventStatus.NOTTAKEN) return;
        questEvent.UpdateQuestEvent(QuestEvent.EventStatus.CURRENT);
        Instantiate(GameAssets.i.questButton, transform.position, Quaternion.identity).GetComponent<QuestButton>().Setup(questEvent, questLog);


    }
}
