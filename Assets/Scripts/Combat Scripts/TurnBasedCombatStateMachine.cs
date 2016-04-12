using UnityEngine;
using System.Collections;

public class TurnBasedCombatStateMachine : MonoBehaviour {

    private bool hasAddedXP = false;
    private int turnNumber = 0;

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
                break;


            case (BattleStates.WIN):
                if (!hasAddedXP)
                {
                    //IncreaseExperience.AddExperience();
                    hasAddedXP = true;
                }
                break;

        }
	}
}
