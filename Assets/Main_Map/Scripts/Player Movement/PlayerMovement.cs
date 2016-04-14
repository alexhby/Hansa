using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    //private GameInfo gameInfo = new GameInfo();
    private Vector3 newPos;
    private string pathID;
    int travelling = 0;
    int searching = 0;
    int speed = 5;
    private GameObject Player;
    private string ID;
    MapHud maphud = new MapHud();
    public GameObject HudContent;
    public GameObject ShopButton;
    public GameObject AreaText;
    public GameObject DecisionPanel;
    int rand;
    private CreateNewQuest newq = new CreateNewQuest();
    //private updateAreas UA = new updateAreas();
    
    
    



    // Use this for initialization
    void Start()
    {
        ID = gameObject.name;
        //Debug.Log("Starting ID is: " + ID);
        Player = GameObject.Find("Puppeteer");
       // WorldInformation.CurrentArea = "1";
        //Debug.Log(WorldInformation.CurrentArea + "THIS BEEETH YOUR CURRENT AREA");
        GameObject currArea = GameObject.Find(WorldInformation.CurrentArea);
        Player.transform.position = currArea.transform.position;
        GameInformation.PlayerMapState = GameInformation.PlayerMapStates.Idle;

    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
                PathTravel();            
        }
    }

    // Update is called once per frame
    

    private void PathTravel()
    {
        //Debug.Log("I'm travelling!111");
        if (String.Compare(WorldInformation.CurrentArea, ID) != 0)
        {
            
            // Debug.Log("I'm travelling!22");
            //if (GameInformation.PlayerMapState == GameInformation.PlayerMapStates.Idle)
            //{
            //while (travelling == 1) ;
            GameInformation.PlayerMapState = GameInformation.PlayerMapStates.Travelling;
                int edgeFinder = 0;
                GameObject v;
                int tempID = 50;
                float tempDist = 1000f;
                //Debug.Log("Your current area:  " + WorldInformation.CurrentArea);
                while (edgeFinder < 5 && WorldInformation.Edges[Int32.Parse(WorldInformation.CurrentArea) - 1, edgeFinder] != 0)
                {
                    //Debug.Log("Just saw that you have an edge to " + WorldInformation.Edges[Int32.Parse(WorldInformation.CurrentArea) - 1, edgeFinder]);
                    v = GameObject.Find(WorldInformation.Edges[Int32.Parse(WorldInformation.CurrentArea) - 1, edgeFinder] + "");
                    //Debug.Log("The obj dist is: " + Vector3.Distance(v.transform.position, gameObject.transform.position));
                    if (tempDist >= Vector3.Distance(v.transform.position, gameObject.transform.position))
                    {
                        tempDist = Vector3.Distance(v.transform.position, gameObject.transform.position);
                        tempID = WorldInformation.Edges[Int32.Parse(WorldInformation.CurrentArea) - 1, edgeFinder];
                        // Debug.Log("Your tempID is:  " + tempID);
                    }
                    edgeFinder++;
                }
            rand = WorldInformation.rnd.Next(0, 11);
            Debug.Log(rand + "IS YOUR RANDOM ENCOUNTER number");
            if (rand < 0)
            {
                //Scene Switch! with GameInformation.PlayerCharacter.PlayerLevel    Random encounter
                Quest randomBatteQuest = newq.returnQuest();
                randomBatteQuest.QuestType = Quest.QuestTypes.Random;
                randomBatteQuest.QuestName = "Random Encounter!";
                randomBatteQuest.QuestLocation = WorldInformation.Areas.Find(x => x.IconNumber == Int32.Parse(WorldInformation.CurrentArea));
                
                WorldInformation.CurrentQuest = randomBatteQuest;

                if (WorldInformation.CurrentQuest.QuestLocation.AreaType == Area.AreaTypes.Plains)
                {
                    SceneManager.LoadScene("Combat1");
                }
                else if (WorldInformation.CurrentQuest.QuestLocation.AreaType == Area.AreaTypes.Desert)
                {
                    SceneManager.LoadScene("Combat3");
                }
                else
                {
                    SceneManager.LoadScene("Combat2");
                }


                Debug.Log("BATTLE!!!!!!! :D");
            }
            else {
                travel(WorldInformation.CurrentArea, tempID + "");
            }
                GameInformation.PlayerMapState = GameInformation.PlayerMapStates.Idle;
            //}
        }
        else
        {
            // Debug.Log("YOU'VE ARRIVED AT YOU DESTINATION!!!!");
           maphud.LoadAreaOptions(ShopButton, HudContent,AreaText,DecisionPanel);
           
            //

            
           
            
        }
    }


    private void travel(string A, string B)
    {
        
            int curr = Int32.Parse(A);
            int dest = Int32.Parse(B);
            //newPos = transform.position;
            if (curr != dest )
            {
                if (curr < dest)
                {

                    //Debug.Log("test!");
                    pathID = A + "to" + B;
                    //Debug.Log("Your pathID :" + pathID);

                    iTween.MoveTo(Player, iTween.Hash("path", iTweenPath.GetPath(pathID), "time", 2, "orienttopath", true, "easetype", iTween.EaseType.linear, "oncompletetarget", gameObject, "onComplete", "PathTravel"));
                    WorldInformation.CurrentArea = B;
                    //Debug.Log("The new current ID: " + WorldInformation.CurrentArea);
                    //Player.transform.position = GameObject.Find(B).transform.position;
                }
                else
                {
                    //travelling = 1;
                    //Debug.Log("test!");
                    pathID = B + "to" + A;
                    //Debug.Log("Your pathID :" + pathID);
                    iTween.MoveTo(Player, iTween.Hash("path", iTweenPath.GetPathReversed(pathID), "time", 2, "orienttopath", true, "easetype", iTween.EaseType.linear, "oncompletetarget", gameObject, "onComplete", "PathTravel"));
                    WorldInformation.CurrentArea = B;
                    //Debug.Log("The new current ID: " + WorldInformation.CurrentArea);
                    //Player.transform.position = GameObject.Find(B).transform.position;
                }
                //travelling = 0;
            }
        
    }

    
}


