using System;
using System.Collections.Generic;
using TileDraw.Map;
using UnityEngine;

public class PathFinding : MonoBehaviour
{

    //***PATHFINDING -- MOVEMENT***
    //A* ALGORITHM

    public Transform start;
    public Transform target;
    public float maxMoveDistance;
    public float maxClimbHeight;
    public List<Tile> mPath;
    public List<Tile> availMoveTiles;
    private Cell c;

    void Start()
    {
        c = transform.Find("(0,0)").GetComponent<Cell>();
    }


    public void showAvailableMoves(Vector3 startPos, int agility)
    {
        Vector2 t = c.convertWorldPosToIndex(startPos.x, startPos.z);
        Tile startTile = c.GetTileFromPointInCell((int)t.x, (int)t.y);
        List<Tile> openSet = new List<Tile>();

        //.........MAX MOVE AND CLIMB CALCULATIOMS  ......................
        maxMoveDistance = 5 + ((agility / 100) * (20));
        maxClimbHeight = 0.25f + ((agility / 100) * 4.75f);

        openSet.Add(startTile);
        startTile.gCost = 0;
        //Debug.Log("start tile at index [" + t.x + "," + t.y + "]"); //DEBUG

        while (openSet.Count > 0)
        {
            Tile currentTile = openSet[0];
            Vector2 p = c.GetPointInCellFromTileIndex(currentTile.TileIndex);
            Vector2 currentTilePos = c.convertIndexToWorldPos((int)p.x, (int)p.y);
            openSet.Remove(currentTile);

            if (currentTile.EntityIndex == -1 && currentTile.EntityString == "" && currentTile != startTile)
            {
                //Debug.Log("Avail tile at index [" + wp.x + "," + wp.y + "]"); //DEBUG
                Instantiate(Resources.Load("GreenPrj"), new Vector3(currentTilePos.x, 1, currentTilePos.y), Quaternion.Euler(90, 0, 0));
                availMoveTiles.Add(currentTile);
            }

            else if (currentTile != startTile)
                continue;

            if ( currentTile != startTile)
                currentTile.EntityString = "canMoveHere";

            foreach (string str in (currentTile.Neighbours))
            {
                Tile neighbour;
                //Cant move over the edge or onto another character
                if (str != "None")
                {
                    neighbour = c.ConvertStringToTile(str);

                    if (neighbour.EntityString.Length > 3 && neighbour.EntityString.Contains("Character"))
                    {
                        //Debug.Log("Tile : " + str + "has a character");
                        continue;
                    }

                    int gCostNeighbour = currentTile.gCost + GetDistance(currentTile, neighbour);
                    if (gCostNeighbour <= (maxMoveDistance * 10) && !openSet.Contains(neighbour) && neighbour.GetHeight() <= maxClimbHeight)
                    {
                        neighbour.gCost = gCostNeighbour;
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }
    /*Find a Path
     *@param startPos: the starting position.
     *@param targetPos: the end position.
    */
    public int FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector2 t1 = c.convertWorldPosToIndex(startPos.x, startPos.z);
        Vector2 t2 = c.convertWorldPosToIndex(targetPos.x, targetPos.z);
        Tile startTile = c.GetTileFromPointInCell((int)t1.x, (int)t1.y);
        Tile targetTile;
        
        try
        {
            targetTile = c.GetTileFromPointInCell((int)t2.x, (int)t2.y);
        }
        catch (Exception e)
        {
            Debug.Log("Error from pathfinding class : " + e.ToString());
            return -1;
        }
        

        if (targetTile.EntityString != "canMoveHere")
        {
            return -1;
        }

        List<Tile> openSet = new List<Tile>();  //Set we are still evaluating
        HashSet<Tile> closedSet = new HashSet<Tile>();  //Set we arent evaluating (Already visited)
        startTile.gCost = 0;
        openSet.Add(startTile);
        //Debug.Log("target tile at index [" + t2.x + "," + t2.y + "]"); //DEBUG

        while (openSet.Count > 0)
        {
            Tile currentTile = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentTile.fCost || openSet[i].fCost == currentTile.fCost && openSet[i].hCost < currentTile.hCost)
                {
                    if(currentTile.EntityString == "canMoveHere")
                        currentTile = openSet[i];
                }
            }
            openSet.Remove(currentTile);
            closedSet.Add(currentTile);

            if (currentTile == targetTile)
            {
                RetracePath(startTile, targetTile);
                return 0;
            }

            foreach (string str in (currentTile.Neighbours))
            {
                Tile neighbour;
                if (str != "None")
                {
                    neighbour = c.ConvertStringToTile(str);

                    if (neighbour.EntityIndex != -1 || closedSet.Contains(neighbour) || neighbour.EntityString != "canMoveHere")
                        continue;

                    int gCostNeighbour = currentTile.gCost + GetDistance(currentTile, neighbour);
                    if (gCostNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = gCostNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetTile);
                        neighbour.parent = currentTile;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
        }
        return -2; //No Path found?
    }
    //RETRACE PATH TO START NODE
    void RetracePath(Tile start, Tile end)
    {
        List<Tile> path = new List<Tile>();
        Tile current = end;

        while (current != start)
        {
            path.Add(current);
            Vector2 p = c.GetPointInCellFromTileIndex(current.TileIndex);
            Vector2 currentTilePos = c.convertIndexToWorldPos((int)p.x,(int)p.y);
            Instantiate(Resources.Load("PrjPath"), new Vector3(currentTilePos.x, 1, currentTilePos.y), Quaternion.Euler(90, 0, 0));
            current = current.parent;
        }
        path.Reverse();
        mPath = path;
    }
    //DISTANCE BETWEEN TWO TILES
    public int GetDistance(Tile t1, Tile t2)
    {
        Vector2 pos1 = c.GetPointInCellFromTileIndex(t1.TileIndex);
        Vector2 pos2 = c.GetPointInCellFromTileIndex(t2.TileIndex);
        int dstX = Mathf.Abs((int)pos1.x - (int)pos2.x);
        int dstY = Mathf.Abs((int)pos1.y - (int)pos2.y);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
