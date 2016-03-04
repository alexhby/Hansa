using UnityEngine;
using System.Collections;
using System;
using System.Linq;
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
    



    // Use this for initialization
    void Start()
    {
        ID = gameObject.name;
        //Debug.Log("Starting ID is: " + ID);
        Player = GameObject.Find("Puppeteer");

        GameInformation.CurrentArea = "1";


    }
    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            PathTravel();
            
        }
    }

    // Update is called once per frame
    

    void PathTravel()
    {
        if (String.Compare(GameInformation.CurrentArea, ID) != 0)
        {
            //while (travelling == 1) ;
            int edgeFinder = 0;
            GameObject v;
            int tempID = 50;
            float tempDist = 1000f;
            //Debug.Log("Your current area:  " + GameInformation.CurrentArea);
            while (edgeFinder < 5 && GameInformation.Edges[Int32.Parse(GameInformation.CurrentArea) - 1, edgeFinder] != 0)
            {
                //Debug.Log("Just saw that you have an edge to " + GameInformation.Edges[Int32.Parse(GameInformation.CurrentArea) - 1, edgeFinder]);
                v = GameObject.Find(GameInformation.Edges[Int32.Parse(GameInformation.CurrentArea) - 1, edgeFinder] + "");
                //Debug.Log("The obj dist is: " + Vector3.Distance(v.transform.position, gameObject.transform.position));
                if (tempDist >= Vector3.Distance(v.transform.position, gameObject.transform.position))
                {
                    tempDist = Vector3.Distance(v.transform.position, gameObject.transform.position);
                    tempID = GameInformation.Edges[Int32.Parse(GameInformation.CurrentArea) - 1, edgeFinder];
                   // Debug.Log("Your tempID is:  " + tempID);
                }
                edgeFinder++;
            }
            travel(GameInformation.CurrentArea, tempID + "");
        }
        else
        {
           // Debug.Log("YOU'VE ARRIVED AT YOU DESTINATION!!!!");
        }
    }


    void travel(string A, string B)
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
                
                iTween.MoveTo(Player, iTween.Hash("path", iTweenPath.GetPath(pathID), "time", 5,"orienttopath",true, "easetype", iTween.EaseType.linear, "oncompletetarget", gameObject, "onComplete", "PathTravel"));
                GameInformation.CurrentArea = B;
                //Debug.Log("The new current ID: " + GameInformation.CurrentArea);
                //Player.transform.position = GameObject.Find(B).transform.position;
            }
            else
            {
                //travelling = 1;
                //Debug.Log("test!");
                pathID = B + "to" + A;
                //Debug.Log("Your pathID :" + pathID);
                iTween.MoveTo(Player, iTween.Hash("path", iTweenPath.GetPathReversed(pathID), "time", 5, "orienttopath", true, "easetype", iTween.EaseType.linear, "oncompletetarget", gameObject, "onComplete", "PathTravel"));
                GameInformation.CurrentArea = B;
                //Debug.Log("The new current ID: " + GameInformation.CurrentArea);
                //Player.transform.position = GameObject.Find(B).transform.position;
            }
            //travelling = 0;
        }
    }

    void endPosition()
    {

    }
}


