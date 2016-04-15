using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

//Function Alters and controls the map hud based on the players movement -- Controls the View of the HUD 
//Shows available quests in specific areas
public class MapHud : MonoBehaviour {
    //instantiates the area options off of quest button prefabs
    public void LoadAreaOptions(GameObject ShopButton, GameObject HUDContent, GameObject AreaText, GameObject DecisionPanel)
    {
        int index = 0;
        int height = -65;

        //iterate through areas in area list to find area options
        Area myArea = WorldInformation.Areas.Find(x => x.IconNumber == Int32.Parse(WorldInformation.CurrentArea));
        
        //Delete quests from other area
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in HUDContent.transform) if(String.Compare(child.gameObject.name,"Button")!=0)children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
        Text tx = AreaText.GetComponent<Text>();
        
        //show the current area name and owner
        tx.text = myArea.AreaName + " \n("+myArea.OwnedBy.KingName+")";
        if (myArea.AreaType == Area.AreaTypes.City)
        {
            //Show shop option!
            ShopButton.SetActive(true);
            height = -150;

        }
        else
        {
            ShopButton.SetActive(false);
        }

        //Iterate through current quests
        //iterate through quest locations in current quests to find options
        for (int i = 0; i < GameInformation.PlayerQuestLog.CurrentQuests.Count; i++)
        {
            if (GameInformation.PlayerQuestLog.CurrentQuests[i].QuestLocation.IconNumber == myArea.IconNumber)
            {
               
                //Show current quests in the right area!
                GameObject newB = (GameObject)Instantiate(Resources.Load("QuestButtonMainHuD"));
                newB.transform.SetParent(HUDContent.transform);
                SetListener(newB.GetComponent<Button>(), index, GameInformation.PlayerQuestLog.CurrentQuests[i],DecisionPanel);
                index++;
                RectTransform ButtonRect = (RectTransform)newB.transform;
                ButtonRect.anchoredPosition3D = new Vector3(0, height, 0);
                ButtonRect.localScale = new Vector3(1, 1, 1);

                GameObject Name = newB.transform.GetChild(0).gameObject;
                Text text;
                text = Name.GetComponent<Text>();
                text.text = GameInformation.PlayerQuestLog.CurrentQuests[i].QuestName;

                Name = newB.transform.GetChild(1).gameObject;
                text = Name.GetComponent<Text>();

                text.text = "Reward: " + GameInformation.PlayerQuestLog.CurrentQuests[i].GoldReward + " Gold";
                if (GameInformation.PlayerQuestLog.CurrentQuests[i].EquipmentReward != null)
                {
                    text.text += ", 1 piece of Equipment";
                }
                if (GameInformation.PlayerQuestLog.CurrentQuests[i].WeaponReward != null)
                {
                    text.text += ", 1 Weapon";
                }
                if (GameInformation.PlayerQuestLog.CurrentQuests[i].PotionReward != null)
                {
                    text.text += ", 1 Potion";
                }

                height = height - 100;
            }
        }

        height = height - 50;
        //Resizes the window based on how many items the player/shop has
        RectTransform ViewRect = (RectTransform)HUDContent.transform;
        ViewRect.sizeDelta = new Vector2(0, height * (-1));




    }


    private static void SetListener(Button B, int index, Quest q, GameObject DP)
    {
        //Adds a listener onto a button 
        int i = index;
        B.onClick.AddListener(delegate { StartQuest(i, q, DP); });

    }

    private static void StartQuest(int i, Quest q, GameObject DP)
    {
        GameInformation.PlayerMapState = GameInformation.PlayerMapStates.Travelling;
        //Call the scene switch with an active quest
        WorldInformation.CurrentQuest = q;
        //Debug.Log("Quest activated!");

        //scene switch
        if(q.QuestType == Quest.QuestTypes.Control)
        {
            WorldInformation.Control = q;
            //set decision panel to active
            DP.SetActive(true);
            GameObject titleText = DP.transform.GetChild(2).gameObject;
            Text texty = titleText.GetComponent<Text>();
            texty.text = q.QuestAlliance.KingName + "'s territory " + q.QuestLocation.AreaName + " is being taken over by " + q.QuestEnemy.KingName;
            
        }
        else
        {
            GameInformation.PlayerMapState = GameInformation.PlayerMapStates.Idle;
            if (q.QuestLocation.AreaType == Area.AreaTypes.Plains)
            {
                SceneManager.LoadScene("Combat1");
            }
            else if (q.QuestLocation.AreaType == Area.AreaTypes.Desert)
            {
                SceneManager.LoadScene("Combat3");
            }
            else
            {
                SceneManager.LoadScene("Combat2");
            }
        }
    }

    //Choosing attack after selecting control event panel
    public void AttackControl()
    {
        GameInformation.PlayerMapState = GameInformation.PlayerMapStates.Idle;
        WorldInformation.attacker = 0;
        if (WorldInformation.Control.QuestLocation.AreaType == Area.AreaTypes.Plains)
        {
            SceneManager.LoadScene("Combat1");
        }
        else if (WorldInformation.Control.QuestLocation.AreaType == Area.AreaTypes.Desert)
        {
            SceneManager.LoadScene("Combat3");
        }
        else
        {
            SceneManager.LoadScene("Combat2");
        }
        Debug.Log("Attack");

    }

    //Choosing attack after selecting control event panel
    public void DefendControl()
    {
        GameInformation.PlayerMapState = GameInformation.PlayerMapStates.Idle;
        WorldInformation.attacker = 1;
        if (WorldInformation.Control.QuestLocation.AreaType == Area.AreaTypes.Plains)
        {
            SceneManager.LoadScene("Combat1");
        }
        else if (WorldInformation.Control.QuestLocation.AreaType == Area.AreaTypes.Desert)
        {
            SceneManager.LoadScene("Combat3");
        }
        else
        {
            SceneManager.LoadScene("Combat2");
        }
        Debug.Log("Defend");
    }
    public void Shop()
    {
        SceneManager.LoadScene("Store");
    }
    
	
	// Update is called once per frame
	void Update () {
	
	}

    
}
