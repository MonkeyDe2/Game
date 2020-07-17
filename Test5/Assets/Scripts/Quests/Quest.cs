using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest 
{
    public List<QuestEvent> questEvents = new List<QuestEvent>();

    public Quest() {}

    public QuestEvent AddQuestEvent(string n, string d, string[] dependencies, GameObject[] questIcons)
    {
        QuestEvent questEvent = new QuestEvent(n,d,dependencies, questIcons);
        questEvents.Add(questEvent);
        return questEvent;
    }


    public QuestEvent FindQuestEvent(string name)
    {
        foreach(QuestEvent n in questEvents)
        {
            if (n.name == name)
                return n;
        }
        return null;
    }


    public void PrintPath()
    {
        foreach (QuestEvent n in questEvents)
        {
            Debug.Log(n.name +  " " + n.status);
        }
    }

}
