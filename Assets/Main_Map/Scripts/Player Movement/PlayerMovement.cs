using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

//Governs the icons that must be clicked to move to the new area 
//implements greedy algorithm for path travelling
//Refers to the itween paths that have been preset by yours truly
public class PlayerMovement : MonoBehaviour
{
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
    
    // Use this for initialization
    void Start()
    {
        ID = gameObject.name;
        Player = GameObject.Find("Puppeteer");
        GameObject currArea = GameObject.Find(WorldInformation.CurrentArea);
        Player.transform.position = currArea.transform.position;
        GameInformation.PlayerMapState = GameInformation.PlayerMapStates.Idle;

    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
                //Will travel upon clicking a new area
                PathTravel();            
        }
    }

    // Update is called once per frame
    

    private void PathTravel()
    {
        
        if (String.Compare(WorldInformation.CurrentArea, ID) != 0)
        {
            
            GameInformation.PlayerMapState = GameInformation.PlayerMapStates.Travelling;
                int edgeFinder = 0;
                GameObject v;
                int tempID = 50;
                float tempDist = 1000f;

                //maps out the closest edge (edges are stored in the worldinformation) to the destination area
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
            
            if (rand < 2)
            {
                //Scene Switch! with GameInformation.PlayerCharacter.PlayerLevel    Random encounter
                //Saves this encounter as a quest to transfer relevant battle data to battle scene
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

                GameInformation.PlayerMapState = GameInformation.PlayerMapStates.Idle;
                Debug.Log("Random BATTLE!!!!!!! :D");
            }
            else {
                travel(WorldInformation.CurrentArea, tempID + "");
            }
                
            //}
        }
        else
        {
            // Debug.Log("YOU'VE ARRIVED AT YOU DESTINATION!!!!");
           maphud.LoadAreaOptions(ShopButton, HudContent,AreaText,DecisionPanel);
            GameInformation.PlayerMapState = GameInformation.PlayerMapStates.Idle;

            //




        }
    }

    //Calls the iTween path from area A to area B (these are adjacent areas)
    private void travel(string A, string B)
    {
        
            int curr = Int32.Parse(A);
            int dest = Int32.Parse(B);
            //newPos = transform.position;
            if (curr != dest )
            {
            //increments the day counter and after 20 days have gone by, renew all items and quests in shop
            WorldInformation.DayCounter = WorldInformation.DayCounter + 1;
            if(WorldInformation.DayCounter > 20)
            {
                WorldInformation.initShopsAndQuests();
                WorldInformation.DayCounter = 0;
            }
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


