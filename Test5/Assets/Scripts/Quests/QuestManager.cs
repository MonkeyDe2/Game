using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public Quest quest = new Quest();

    [SerializeField] private QuestTemplate[] questTemplate;

    public QuestTemplate[] QuestTemplates
    {
        get{ return questTemplate;}
    }
    
    void Start()
    {

        foreach (QuestTemplate qtemp in questTemplate){
            quest.AddQuestEvent(qtemp.Name,qtemp.Description,qtemp.Dependencies, qtemp.MyQuestIcon);
            quest.FindQuestEvent(qtemp.Name).OnStatusUpdate += Listener_OnStatusUpdate;
            

            qtemp.Giver.GetComponent<QuestGiver>().Setup(this, quest.FindQuestEvent(qtemp.Name));
            qtemp.Receiver.GetComponent<QuestLocation>().Setup(this, quest.FindQuestEvent(qtemp.Name));
        }
        
    }

    // void Update()
    // {
    //     quest.PrintPath();
    // }

    public void UpdateQuestsOnCompletion(QuestEvent e)
    {
        foreach (QuestEvent n in quest.questEvents)
        {

            if (n.status == QuestEvent.EventStatus.NOTAVAILABLE){
                if (n.dependencies.Length != 0){
                    List<string> list = new List<string>(n.dependencies);
                    if (list.Contains(e.name)){
                        list.Remove(e.name);
                        n.dependencies = list.ToArray();
                    }
                        
                }

                if (n.dependencies.Length == 0){
                    n.UpdateQuestEvent(QuestEvent.EventStatus.NOTTAKEN);
                } 
            }
        }
    }

    public void Listener_OnStatusUpdate(object sender, QuestEvent.OnStatusUpdateEventArgs e){
        foreach (QuestTemplate qtemp in questTemplate)
        {
            if (qtemp.Name == e.name)
            {
                if (qtemp.MyQuestIcon == null || qtemp.MyQuestIcon.Length == 0) return;

                if (e.status == QuestEvent.EventStatus.CURRENT){
                    qtemp.MyQuestIcon[0].GetComponent<QuestIcon>().UpdateIcon(2);
                    qtemp.MyQuestIcon[1].GetComponent<QuestIcon>().UpdateIcon(0);
                } else if (e.status == QuestEvent.EventStatus.NOTTAKEN)
                {
                    qtemp.MyQuestIcon[0].GetComponent<QuestIcon>().UpdateIcon(1);
                    qtemp.MyQuestIcon[1].GetComponent<QuestIcon>().UpdateIcon(2);
                } else
                {
                    qtemp.MyQuestIcon[0].GetComponent<QuestIcon>().UpdateIcon(2);
                    qtemp.MyQuestIcon[1].GetComponent<QuestIcon>().UpdateIcon(2);
                } 
                
            }
        }
    }
}

[Serializable]
public class QuestTemplate
{
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private GameObject giver;
    [SerializeField] private GameObject receiver;
    [SerializeField] private GameObject[] questIcon;
    [SerializeField] private string[] dependencies;

    public String Name {
    get{
      return name;
        }
    }

    public String Description {
    get{
      return description;
        }
 
    }

    public GameObject Giver {
    get{
      return giver;
        }

    }
    public GameObject Receiver {
    get{
      return receiver;
        }

    }
    public String[] Dependencies {
    get{
      return dependencies;
        }

    }
    public GameObject[] MyQuestIcon {
    get{
      return questIcon;
        }

    }
}


