using UnityEngine;
using System.Collections;
using LitJson;
using System;


//!!! IMPORTANT FUNCTION: does all database updates 
//namely this script will increment the control if successfully returning from a control event (upon 3 wins the control is switched)
//Initializes a new control event if over an hour has passed since the last initiation
//Finally after all the previous changes, this script then retrieves all the area info and updates the area accordingly

public class updateAreas: MonoBehaviour {
    private static  string url = "http://tomaswolfgang.com/hansa361/GetWorldInformation.php";
    private static string url2 = "http://tomaswolfgang.com/hansa361/InitControlEvent.php";
    private static string url3 = "http://tomaswolfgang.com/hansa361/IncrementControl.php";
    private static JsonData AreaData;
    private Area targetArea;
    
   void Start()
    {
        bool Attacked = false;
        
        //INIT NEW CONTROL EVENT
        WWWForm form2 = new WWWForm();
        int targetIcon = WorldInformation.rnd.Next(1, 34);
        targetArea = WorldInformation.Areas.Find(x => x.IconNumber == targetIcon);
        form2.AddField("world", WorldInformation.currentWorldID);
        form2.AddField("area",targetArea.AreaID); 
        for(int i = 0; i< 5; i++)
        {
            if (WorldInformation.Edges[targetArea.IconNumber - 1, i] != 0 && !Attacked)
            {
                Area enemyArea = WorldInformation.Areas.Find(x => x.IconNumber == WorldInformation.Edges[targetArea.IconNumber - 1, i]);
               
                if (String.Compare(enemyArea.OwnedBy.KingdomID, targetArea.OwnedBy.KingdomID) != 0)
                {
                    Attacked = true;
                    //Debug.Log(enemyArea.AreaName + "is trying to attack " + targetArea.AreaName);
                    form2.AddField("enemy", enemyArea.OwnedBy.KingdomID);
                    WWW www2 = new WWW(url2, form2);
                    StartCoroutine(initControl(www2, enemyArea));
                    
                }
            }
        }

        //UPDATE CONTROL EVENT
        //The current quest is only removed upon successful completion of the quest -- it will only remain if the battle was lost
        bool attacksuccess;
        if (WorldInformation.CurrentQuest == null) attacksuccess = true;
        else attacksuccess = false;

        WorldInformation.CurrentQuest = null;
        if (attacksuccess && WorldInformation.Control != null)
        {
            WWWForm form3 = new WWWForm();
            form3.AddField("world", WorldInformation.currentWorldID);
            //attacker is "0" for attack "1" for defend
            form3.AddField("attackerSuccess",WorldInformation.attacker);
            form3.AddField("area", WorldInformation.Control.QuestLocation.AreaID);
            WWW www3 = new WWW(url3, form3);
            StartCoroutine(updateControl(www3));

        }

        //UPDATE WORLD
        //The final update after all the control events have been updated
        WWWForm form = new WWWForm();
        form.AddField("World", WorldInformation.currentWorldID);
        WWW www = new WWW(url, form);
        StartCoroutine(updateWorld(www));
    }

    IEnumerator updateControl(WWW www)
    {
        //sets the control to null -- 
        yield return www;
        WorldInformation.Control = null;
        WorldInformation.attacker = 0;
        //Debug.Log("your control update: "+www.text);
    }

    IEnumerator initControl(WWW www, Area enemy)
    {
        yield return www;
        //Debug.Log("Control Event Started: ---" + www.text);
        if(Int32.Parse(www.text) == 0)
        {
            initControlQuest(enemy.OwnedBy, targetArea);
                    
        }
    }

    //sets all quest values for this control quest
    private void initControlQuest(Kingdom enemy, Area target)
    {
        Debug.Log("There is a control event happening at" + target.AreaName);
        target.BeingTakenOverBy = enemy;
        Quest ControlEvent = new Quest();
        ControlEvent.QuestType = Quest.QuestTypes.Control;

        ControlEvent.QuestAlliance = target.OwnedBy;
        ControlEvent.QuestLocation = target;
        ControlEvent.QuestEnemy = enemy;


        ControlEvent.QuestName = "Control: " + ControlEvent.QuestLocation.AreaName + "!";
        ControlEvent.QuestDescription = ControlEvent.QuestLocation.AreaName + " has fallen into a state of disarray and " + ControlEvent.QuestEnemy.KingName + " is making a move!";

        ControlEvent.RecommendedLevel = GameInformation.PlayerCharacter.PlayerLevel;
        ControlEvent.GoldReward = ControlEvent.RecommendedLevel * ControlEvent.RecommendedLevel + 300;

        GameInformation.PlayerQuestLog.CurrentQuests.Add(ControlEvent);
    }

    IEnumerator updateWorld(WWW www)
    {
        //After updating the world - initializes control events that are already going on
        yield return www;
        GameInformation.PlayerQuestLog.CurrentQuests.RemoveAll(x => x.QuestType == Quest.QuestTypes.Control);
        AreaData = JsonMapper.ToObject(www.text);
        for (int i = 0; i < 34; i++)
        {
            //Debug.Log("Updating yo SHIT");
            Area holder = WorldInformation.Areas.Find(x => String.Compare(x.AreaID, AreaData[i]["area_ID"].ToString()) == 0);
            holder.OwnedBy = WorldInformation.Kingdoms.Find(x => String.Compare(x.KingdomID, AreaData[i]["owner_kingdom_ID"].ToString()) == 0);
            holder.BeingTakenOverBy = WorldInformation.Kingdoms.Find(x => String.Compare(x.KingdomID, AreaData[i]["enemy_kingdom_ID"].ToString()) == 0);
            if(Int32.Parse(holder.BeingTakenOverBy.KingdomID) != 0)
            {
                //delete control event quest from current quests
                //Debug.Log("There is a control event happening right MEOW");
                initControlQuest(holder.BeingTakenOverBy, holder);
            }
            holder.TakeOverCount = Int32.Parse(AreaData[i]["takeOverCount"].ToString());
            holder.DefendCount = Int32.Parse(AreaData[i]["DefendCount"].ToString());
        }
    }
}
