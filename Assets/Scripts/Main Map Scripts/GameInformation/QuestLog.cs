using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]

//Questlog -- two lists of quests -- finished quests and current quests (the purposes of these are self explanatory)
public class QuestLog  {
    private string str = "";
	public List<Quest> FinishedQuests { get; set; }
    public List<Quest> CurrentQuests { get; set; }
    public Quest ActiveQuest { get; set; }

    
    //prints used for testing
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
