using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]

public class QuestLog : MonoBehaviour {

	public List<Quest> FinishedQuests { get; set; }
    public List<Quest> CurrentQuests { get; set; }
    public Quest ActiveQuest { get; set; }

    public void printQuest(Quest q)
    {
        Debug.Log(q.QuestType.ToString());
    }

    public void printAllQuests()
    {
        GameInformation.PlayerQuestLog.CurrentQuests.ForEach(printQuest);
    }
	
}
