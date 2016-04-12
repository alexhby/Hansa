using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MapHud : MonoBehaviour {
    

    // Use this for initialization
    void Start()
    {
        
    }


    public void LoadAreaOptions(GameObject ShopButton, GameObject HUDContent)
    {
        int index = 0;
        int height = -15;

        //iterate through areas in area list to find area options
        Area myArea = WorldInformation.Areas.Find(x => x.IconNumber == Int32.Parse(WorldInformation.CurrentArea));
        if (myArea.AreaType == Area.AreaTypes.City)
        {
            //Show shop option!
            ShopButton.SetActive(true);
            height = -100;

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
                SetListener(newB.GetComponent<Button>(), index, GameInformation.PlayerQuestLog.CurrentQuests[i]);
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


    private static void SetListener(Button B, int index, Quest q)
    {
        //Adds a listener onto a button 
        int i = index;
        B.onClick.AddListener(delegate { StartQuest(i, q); });

    }

    private static void StartQuest(int i, Quest q)
    {
        //Call the scene switch with an active quest
        WorldInformation.CurrentQuest = q;
        Debug.Log("Quest activated!");
        //scene switch
    }
    
	
	// Update is called once per frame
	void Update () {
	
	}

    
}
