using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]

//Questlog -- two lists of quests
public class QuestLog  {
    private string str = "";
	public List<Quest> FinishedQuests { get; set; }
    public List<Quest> CurrentQuests { get; set; }
    public Quest ActiveQuest { get; set; }

    

    public void printQuest(Quest q)
    {
        str = str + "   "+q.QuestName;
    }

    public void printAllQuests()
    {
        str = "";
        CurrentQuests.ForEach(printQuest);
        Debug.Log(str);
    }
	
}
