using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOrder : MonoBehaviour
{
    [SerializeField] GameObject[] questCollectDialog;
    [SerializeField] GameObject[] questGiveDialog;
    void OnTriggerEnter2D(Collider2D other)
    {
        foreach(GameObject go in questCollectDialog)
        {
            if (go.GetComponent<QuestLocation>().qEvent.status == QuestEvent.EventStatus.CURRENT)
            {
                go.transform.parent.gameObject.SetActive(true);
                return;
            }
        }
        foreach(GameObject go in questGiveDialog)
        {
            if (go.GetComponent<QuestGiver>().questEvent.status == QuestEvent.EventStatus.NOTTAKEN)
            {
                go.transform.parent.gameObject.SetActive(true);
                return;
            }
        }
    }
}
