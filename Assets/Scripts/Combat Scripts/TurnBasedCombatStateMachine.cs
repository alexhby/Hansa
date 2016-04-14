using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class TurnBasedCombatStateMachine : MonoBehaviour {

    private int turnNumber = 0;
	private Button back;
	private Transform endscene;
	private Text content;
    private Text wl;
    private Transform lg;
    private Transform sv;
    public bool isFresh = true;
    private static LevelUp lu = new LevelUp();
    public static int experienceGain = 40;
    public static int goldPenalty = 500; // minus this if lost
    private bool isLevelUp = false;
    public enum BattleStates {START,PLAYERCHOICE, PLAYERANIMATE,ENEMYCHOICE, LOSE, WIN }
    private BattleStates currentState;
    private WorldInformation winfo;

    public void setCurrentState(int battleState)
    {
        currentState = (BattleStates)battleState;
    }
    public BattleStates getCurrentState()
    {
        return currentState;
    }
  
	// Use this for initialization
	void Start () {

        currentState = BattleStates.PLAYERCHOICE;
		back = transform.Find ("Canvas/Back").GetComponent<Button> ();
		endscene = transform.Find ("Canvas/EndScene");
		content = endscene.Find ("Scroll View/Viewport/Content").GetComponent<Text> ();
        sv = endscene.Find("Scroll View");
        wl = endscene.Find("State").GetComponent<Text>();
        lg = endscene.Find("Loss");
        winfo = GameObject.Find("GameInformation").GetComponent<WorldInformation>();
	}

	private string displayString(BaseCharacter c){


		int oldD = c.Defense;
		int oldA = c.Agility;
		int oldI = c.Intellect;
		int oldS = c.Strength;
		int oldH = c.Health;


		return 
			"\nDefense: " + oldD + "-->" + c.Defense +
			"\nAgility: " + oldA + "-->" + c.Agility +
			"\nIntellect: " + oldI + "-->" + c.Intellect +
			"\nStrength: " + oldS + "-->" + c.Strength +
			"\nHealth: " + oldH + "-->" + c.CurrentHealth +"\n";
	}

	public void nextScene()
	{
        endscene.gameObject.SetActive(false);
        lg.gameObject.SetActive(false);
        sv.gameObject.SetActive(false);

        isLevelUp = false;
        //next.onClick.RemoveAllListeners();
        Debug.Log("ON TO THE NEXT SCENE");
		Application.LoadLevel ("test");
	}

    public void Lose()
    {
        back.gameObject.SetActive(true);
        back.onClick.AddListener(() => nextScene());
        isFresh = false;
        Debug.Log("YOU LOSE -------------------------");
        sv.gameObject.SetActive(false);
        endscene.gameObject.SetActive(true);
        lg.gameObject.SetActive(true);
        wl.text = "You Lose!";
        

        //TODO GUI:
        //You Lose! 
        //Lose 500 gold.

        // if lose, minus gold
        GameInformation.Gold -= goldPenalty;
        if (GameInformation.Gold < 0)
        {
            GameInformation.Gold = 0;
        }

        

    }

    public void Win()
    {
        back.gameObject.SetActive(true);
        back.onClick.AddListener(() => nextScene());
        isFresh = false;
        Debug.Log("YOU WIN -------------------------");
        endscene.gameObject.SetActive(true);
        sv.gameObject.SetActive(true);
        wl.text = "You Win!";
        //string quest = WorldInformation.CurrentQuest.QuestName;
        Debug.Log(WorldInformation.CurrentQuest.QuestName);
        content.text = "You have finished the quest: \n* *\n";
        content.text += WorldInformation.CurrentQuest.QuestName ;
        content.text += "\n* *\nAll characters have gained 40 experience.";

        //TODO GUI:
        //You Win!
        //You have finished the current quest.name and gained all the rewards.
        //Every character gains 40 XP.

        // if win, get the rewards
        GameInformation.Gold += WorldInformation.CurrentQuest.GoldReward;

        if (WorldInformation.CurrentQuest.WeaponReward != null)
        {
            GameInformation.PlayerInventory.Weapons.Add(WorldInformation.CurrentQuest.WeaponReward);
        }
        if (WorldInformation.CurrentQuest.EquipmentReward != null)
        {
            GameInformation.PlayerInventory.Equipment.Add(WorldInformation.CurrentQuest.EquipmentReward);
        }
        if (WorldInformation.CurrentQuest.PotionReward != null)
        {
            GameInformation.PlayerInventory.Potions.Add(WorldInformation.CurrentQuest.PotionReward);
        }

        // move the quest to FinishedQuests
        GameInformation.PlayerQuestLog.FinishedQuests.Add(WorldInformation.CurrentQuest);
        GameInformation.PlayerQuestLog.CurrentQuests.Remove(WorldInformation.CurrentQuest);
        WorldInformation.CurrentQuest = null;

        //Gain XP, check for levelup
        promote(GameInformation.PlayerCharacter);
        promote(GameInformation.Char1);
        promote(GameInformation.Char2);
        promote(GameInformation.Char3);
        promote(GameInformation.Char4);
        promote(GameInformation.Char5);

    }


    //Helper func: Gain XP, check for levelup
    private void promote(BaseCharacter character)
    {
        if (character != null)
        {
            
            character.CurrentXP += experienceGain;
            isLevelUp = lu.LevelUpCharacter(character);
            if (isLevelUp)
            {

                // save old stats for GUI display
                int oldD = character.Defense;
                int oldA = character.Agility;
                int oldI = character.Intellect;
                int oldS = character.Strength;

                // increase stats when level up
                int tier = (int)character.PlayerClass / 4 + 1;

                //basic increase
                character.Defense += tier;
                character.Agility += tier;
                character.Intellect += tier;
                character.Strength += tier;
                character.Health += tier * 20;
                if (character.Health > 100)
                {
                    character.Health = 100;
                }

                //extra increase based on class
                if ((int)character.PlayerClass % 4 == 0)
                {
                    //squire
                    character.Defense += tier;
                    character.Strength += tier;
                }
                else if ((int)character.PlayerClass % 4 == 1)
                {
                    //Apprentice
                    character.Agility += tier;
                    character.Intellect += tier;
                }
                else if ((int)character.PlayerClass % 4 == 2)
                {
                    //Thief
                    character.Agility += tier;
                    character.Strength += tier;

                }
                else
                {
                    //Archer
                    character.Defense += tier;
                    character.Intellect += tier;
                }

                try
                {
                    SaveInformation.SaveAllCharacterInformation();
                }
                catch (Exception e)
                {
                    Debug.Log("Couldnt save character information");
                }

                content.text += "\n* *\nLevel up!\n* *\n" + character.PlayerName +
                "\nDefense: " + oldD + "-->" + character.Defense +
                "\nAgility: " + oldA + "-->" + character.Agility +
                "\nIntellect: " + oldI + "-->" + character.Intellect +
                "\nStrength: " + oldS + "-->" + character.Strength;


            }
        }
    }

	void Update () {

        Debug.Log(currentState);

        switch (currentState)
        {
            //setup functions that run when each state is active.
            case (BattleStates.START):
           
                //setup battle function
                break;

            //Set players turn to true------------------------------------------------------------------------------------------------------
            case (BattleStates.PLAYERCHOICE):

                Transform players = transform.GetChild(0);
                for (int i = 0; i < players.childCount; i++)
                {
                    CharController currentPlayer = players.GetChild(i).GetComponent<CharController>();

                    if (currentPlayer.myTurn)
                    {
                        //Debug.Log("Waiting for player " + i);
                        break; //Dont Continue untill this players turn is over.
                    }

                    if (!currentPlayer.myTurn && turnNumber == i)
                    {
                        currentPlayer.myTurn = true;
                        turnNumber += 1;
                        break;
                    }
                }

                if (turnNumber == players.childCount && !players.GetChild(turnNumber - 1).GetComponent<CharController>().myTurn)
                {
                    turnNumber = 0;
                    currentState = BattleStates.ENEMYCHOICE;
                }

                break;

            //Set enemies turn to true--------------------------------------------------------------------------------------------------------
            case (BattleStates.ENEMYCHOICE):
                //Need Artificial intelligence here
                Transform enemies = transform.GetChild(1);
                for (int i = 0; i < enemies.childCount; i++)
                {
                    CharController currentPlayer = enemies.GetChild(i).GetComponent<CharController>();
                    if (currentPlayer.myTurn)
                        break; //Dont Continue untill this players turn is over.
                    if (!currentPlayer.myTurn && turnNumber == i)
                    {
                        currentPlayer.myTurn = true;
                        turnNumber += 1;
                        break;
                    }
                }

                if (turnNumber == enemies.childCount && !enemies.GetChild(turnNumber - 1).GetComponent<CharController>().myTurn)
                {
                    turnNumber = 0;
                    currentState = BattleStates.PLAYERCHOICE;
                }

                break;
            //LOSS---------------------------------------------------------------------------------------------------------------------------------------
            case (BattleStates.LOSE):
                if (isFresh)
                {
                    Lose();
                }

                currentState = BattleStates.START;
                break;


            case (BattleStates.WIN):
                if (isFresh)
                {
                    Win();
                }
                currentState = BattleStates.START;
                break;

        }
    }
}
