using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvent
{
    public enum EventStatus { NOTAVAILABLE, NOTTAKEN, CURRENT, DONE};

    public string name;
    public string description;
    public EventStatus status;
    public int order = -1;
    public string[] dependencies;
    public GameObject[] questIcons;

    public event EventHandler<OnStatusUpdateEventArgs> OnStatusUpdate;
    public class OnStatusUpdateEventArgs : EventArgs {
        public string name;
        public EventStatus status;
    }

    public QuestEvent(string n, string d, string[] de, GameObject[] qIcons)
    {
        name = n;
        description = d;
        dependencies = de;
        questIcons = qIcons;
        status = EventStatus.NOTAVAILABLE;
    }

    public void UpdateQuestEvent(EventStatus es)
    {
        status = es;
        OnStatusUpdate?.Invoke(this, new OnStatusUpdateEventArgs {name = name, status = status});

    }

}
