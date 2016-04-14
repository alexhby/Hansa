using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//THE QUEST KEEP -- displays the available quests -- allows players to take on quests
public class GetAvailableQuests : MonoBehaviour {

    public GameObject QuestPanel;
    public GameObject QuestList;
    public GameObject QuestKeep;
    private int height = -100;
    private int index;
    void Start()
    {
        //LoadInformation.LoadInventoryInformation();

    }

    public void DisplayAvailableQuests()
    {
        height = -160;
        index = 0;
        
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in QuestList.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
        
        QuestPanel.SetActive(true);
        QuestKeep.SetActive(false);
        //shopBack.SetActive(true);
        //sellButton.SetActive(true);
        //buyButton.SetActive(false);

        Debug.Log("hi \n How are you?");
        //storeOwner.SetActive(false);
        //GameObject.Find("PlayerGold").GetComponent<Text>().text = "Gold: " + GameInformation.Gold;


        WorldInformation.AvailableQuests.ForEach(ShowQuestInStore);
       
    }

    public void ExitAvailableQuests()
    {
        QuestPanel.SetActive(false);
        QuestKeep.SetActive(true);
    }

    public void LeaveShop()
    {
        SaveInformation.SaveInventoryInformation();
        SceneManager.LoadScene("test");
    }
    private void ShowQuestInStore(Quest quest)
    {
        GameObject newB = (GameObject)Instantiate(Resources.Load("QuestButton"));
        newB.transform.SetParent(QuestList.transform);
        SetListener(newB.GetComponent<Button>());
        index++;
        RectTransform ButtonRect = (RectTransform)newB.transform;
        ButtonRect.anchoredPosition3D = new Vector3(0, height, 0);
        ButtonRect.localScale = new Vector3(1, 1, 1);
        height -= 100;
        //The name of the quest
        GameObject Name = newB.transform.GetChild(0).gameObject;
        Text text;
        text = Name.GetComponent<Text>();
        text.text = quest.QuestName;

        //The details of the quest
        Name = newB.transform.GetChild(1).gameObject;
        text = Name.GetComponent<Text>();
        text.text = quest.QuestDescription;

        //The reward of the quest
        Name = newB.transform.GetChild(2).gameObject;
        text = Name.GetComponent<Text>();
        text.text = "Reward: "+ quest.GoldReward +" Gold";
        if(quest.EquipmentReward != null)
        {
            text.text += ", 1 piece of Equipment";
        }
        if (quest.WeaponReward != null)
        {
            text.text += ", 1 Weapon";
        }
        if (quest.PotionReward != null)
        {
            text.text += ", 1 Potion";
        }

    }

    private void SetListener(Button B)
    {
        int i = index;

        B.onClick.AddListener(delegate { TakeQuest(i); });

    }

    private void TakeQuest(int ind)
    {
        
        Quest TakenQuest = WorldInformation.AvailableQuests[ind];

        Debug.Log("You have just taken quest: " + TakenQuest.QuestName);

        GameInformation.PlayerQuestLog.CurrentQuests.Add(TakenQuest);
        WorldInformation.AvailableQuests.Remove(TakenQuest);

        DisplayAvailableQuests();
        
    }
}
