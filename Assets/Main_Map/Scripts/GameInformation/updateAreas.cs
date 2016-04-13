using UnityEngine;
using System.Collections;
using LitJson;
using System;

public class updateAreas: MonoBehaviour {
    private static  string url = "http://tomaswolfgang.com/hansa361/GetWorldInformation.php";
    private static string url2 = "http://tomaswolfgang.com/hansa361/InitControlEvent.php";
    private static JsonData AreaData;
    private Area targetArea;
    
   void Start()
    {
        bool Attacked = false;
        Debug.Log("HEY YOU BIZZTHC");
        WWWForm form = new WWWForm();
        form.AddField("World", WorldInformation.currentWorldID);
        WWW www = new WWW(url, form);

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
                    Debug.Log(enemyArea.AreaName + "is attacking " + targetArea.AreaName);
                    form2.AddField("enemy", enemyArea.OwnedBy.KingdomID);
                    WWW www2 = new WWW(url2, form2);
                    StartCoroutine(initControl(www2, enemyArea));
                    
                }
            }
        }

        

        StartCoroutine(updateWorld(www));
    }

    IEnumerator initControl(WWW www, Area enemy)
    {
        yield return www;
        Debug.Log("Control Event Started: ---" + www.text);
        if(Int32.Parse(www.text) == 0)
        {
            initControlQuest(enemy.OwnedBy, targetArea);
        }
    }

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
        
        yield return www;
        Debug.Log("SUP BIATCH");
        GameInformation.PlayerQuestLog.CurrentQuests.RemoveAll(x => x.QuestType == Quest.QuestTypes.Control);
        AreaData = JsonMapper.ToObject(www.text);
        for (int i = 0; i < 34; i++)
        {
            Debug.Log("Updating yo SHIT");
            Area holder = WorldInformation.Areas.Find(x => String.Compare(x.AreaID, AreaData[i]["area_ID"].ToString()) == 0);
            holder.OwnedBy = WorldInformation.Kingdoms.Find(x => String.Compare(x.KingdomID, AreaData[i]["owner_kingdom_ID"].ToString()) == 0);
            holder.BeingTakenOverBy = WorldInformation.Kingdoms.Find(x => String.Compare(x.KingdomID, AreaData[i]["enemy_kingdom_ID"].ToString()) == 0);
            if(Int32.Parse(holder.BeingTakenOverBy.KingdomID) != 0)
            {
                //delete control event quest from current quests
                Debug.Log("There is a control event happening right MEOW");
                initControlQuest(holder.BeingTakenOverBy, holder);
            }
            holder.TakeOverCount = Int32.Parse(AreaData[i]["takeOverCount"].ToString());
            holder.DefendCount = Int32.Parse(AreaData[i]["DefendCount"].ToString());
        }
    }
}
