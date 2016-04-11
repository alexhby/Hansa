using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;

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
            Debug.Log("I'm travelling!");
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
                travel(WorldInformation.CurrentArea, tempID + "");
                GameInformation.PlayerMapState = GameInformation.PlayerMapStates.Idle;
            //}
        }
        else
        {
            // Debug.Log("YOU'VE ARRIVED AT YOU DESTINATION!!!!");
           maphud.LoadAreaOptions(ShopButton, HudContent);
            
        }
    }


    private void travel(string A, string B)
    {
        int curr = Int32.Parse(A);
        int dest = Int32.Parse(B);
        //newPos = transform.position;
        if (curr != dest)
        {
            if (curr < dest)
            {
                
                //Debug.Log("test!");
                pathID = A + "to" + B;
                //Debug.Log("Your pathID :" + pathID);
                
                iTween.MoveTo(Player, iTween.Hash("path", iTweenPath.GetPath(pathID), "time", 2,"orienttopath",true, "easetype", iTween.EaseType.linear, "oncompletetarget", gameObject, "onComplete", "PathTravel"));
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


