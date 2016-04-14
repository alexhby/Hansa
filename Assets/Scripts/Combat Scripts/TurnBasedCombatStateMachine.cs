using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnBasedCombatStateMachine : MonoBehaviour {

    private bool hasAddedXP = false;
    private int turnNumber = 0;
	private Button next;
	Transform endscene;
	Text content;

    public enum BattleStates {START,PLAYERCHOICE, PLAYERANIMATE,ENEMYCHOICE, LOSE, WIN }

    private BattleStates currentState;

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

        currentState = BattleStates.START;
        hasAddedXP = false;
		next = transform.Find ("Canvas/Next").GetComponent<Button> ();
		endscene = transform.Find ("Canvas/EndScene");
		content = endscene.Find ("Scroll View/Viewport/Content").GetComponent<Text> ();

	}
	private string promote(BaseCharacter c){


		int oldD = c.Defense;
		int oldA = c.Agility;
		int oldI = c.Intellect;
		int oldS = c.Strength;
		int oldH = c.Health;


		return "Level up!\n" + c.PlayerName +
			"\nDefense: " + oldD + "-->" + c.Defense +
			"\nAgility: " + oldA + "-->" + c.Agility +
			"\nIntellect: " + oldI + "-->" + c.Intellect +
			"\nStrength: " + oldS + "-->" + c.Strength +
			"\nHealth: " + oldH + "-->" + c.CurrentHealth +"\n";


	}
	public void clickButton()
	{
		Application.LoadLevel ("test");
	}
	void Update () {

        //Debug.Log(currentState);

        switch (currentState)
        {
            //setup functions that run when each state is active.
            case (BattleStates.START):
                //setup battle function

                currentState = BattleStates.PLAYERCHOICE;
                break;

            //Set players turn to true
            case (BattleStates.PLAYERCHOICE):

                Transform players = transform.GetChild(0);
                for ( int i = 0 ; i < players.childCount; i++ )
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

                if (turnNumber == players.childCount && !players.GetChild(turnNumber-1).GetComponent<CharController>().myTurn)
                {
                    turnNumber = 0;
                    currentState = BattleStates.ENEMYCHOICE;
                }

                break;

            //Set enemies turn to true
            case (BattleStates.ENEMYCHOICE):
                //Need Artificial intelligence here
                Transform enemies = transform.GetChild(1);
                for ( int i = 0 ; i < enemies.childCount; i++ )
                {
                    CharController currentPlayer = enemies.GetChild(i).GetComponent<CharController>();
                    if ( currentPlayer.myTurn)
                        break; //Dont Continue untill this players turn is over.
                    if (!currentPlayer.myTurn && turnNumber == i)
                    {
                        currentPlayer.myTurn = true;
                        turnNumber += 1;
                        break;
                    }                   
                }

                if (turnNumber == enemies.childCount && !enemies.GetChild(turnNumber-1).GetComponent<CharController>().myTurn)
                {
                    turnNumber = 0;
                    currentState = BattleStates.PLAYERCHOICE;
                }

                break;

		case (BattleStates.LOSE):
			Debug.Log ("YOU LOSE -------------------------");
			EndCombat.Lose ();
			Text wl = endscene.Find ("WinLoss").GetComponent<Text> ();
			Text lg = endscene.Find ("LoseGold").GetComponent<Text> ();
			wl.text = "You Lose!";
			next.onClick.AddListener(() => clickButton());
            break;


		case (BattleStates.WIN):
			Debug.Log ("YOU WIN -------------------------");
			next.onClick.AddListener (() => clickButton ());

			content.text = promote (GameInformation.PlayerCharacter)
			+ promote (GameInformation.Char1)
			+ promote (GameInformation.Char2)
			+ promote (GameInformation.Char3)
			+ promote (GameInformation.Char4)
			+ promote (GameInformation.Char5);


            break;

        }
	}
}
