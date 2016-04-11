using System;
using System.Collections.Generic;
using TileDraw.Map;
using UnityEngine;

[Serializable]
public class Cell : MonoBehaviour
{
    public Tile[] Tiles;
    private bool dontDraw;
    private BattleGUI bgui;
    [SerializeField] private int _numberOfTiles;

    void Start()
    {
        UpdateNeighbours();
        UpdateEntities();
        bgui = transform.parent.Find("Canvas").GetComponent<BattleGUI>();
    }

    void Update()
    {

        //DISPLAY WHERE MOUSE POINTER IS
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 2000, Color.magenta, 10);
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log("Collision at [" + hit.point.x + "," + hit.point.z + "]"); //DEBUG
            Vector2 point = convertWorldPosToIndex(hit.point.x, hit.point.z);
            Tile t = null;
            dontDraw = false;
            //Catch wront collisions exceptions
            try
            {
                t = GetTileFromPointInCell((int)point.x, (int)point.y);
            }
            catch (Exception e)
            {
                Debug.Log("Error from Cell class : " + e.ToString());
                dontDraw = true;
            }
            if (dontDraw == false)
            {
                
                Vector2 worldPos = convertIndexToWorldPos((int)point.x, (int)point.y);
                if (t.EntityIndex == -1) // Only instantiate projector if there is no entity there (player cant move to occupied tile)
                    Instantiate(Resources.Load("Projector"), new Vector3(worldPos.x, 1, worldPos.y), Quaternion.Euler(90, 0, 0));
                //Set current enemy target to be displayed upon mouse click
                if (t.EntityString.Contains("enemy") && Input.GetMouseButtonDown(1))
                {
                    //Debug.Log("1: [" + (int)worldPos.x + "," + (int)worldPos.y + "," + (t.GetHeight() + 0.05f) +  "]"); //DEBUG
                    bgui.currentEnemyPos = new Vector3(worldPos.x, t.GetHeight() + 0.05f, worldPos.y);
                }
            }   
        }
    }

    //***CELL GRID FUNCTIONS***
    public Vector2 convertIndexToWorldPos(int x, int y)
    {
        int xPos = x;
        int yPos = y;
     
        xPos = -(_numberOfTiles / 2) + (xPos);
        yPos = -(_numberOfTiles / 2) + (yPos);
        return new Vector2(xPos, yPos);
    }
    public Vector2 convertWorldPosToIndex(float x, float y)
    {
        int xPos = (int)Math.Round(x);
        int yPos = (int)Math.Round(y);
        //Debug.Log("2: [" + (int)xPos + "," + yPos + "]"); //DEBUG
      
        xPos = xPos + (_numberOfTiles / 2);
        yPos = yPos + (_numberOfTiles / 2);

        return new Vector2(xPos, yPos);
    }
    public void GenerateTiles(int numberOfTiles)
    {
        _numberOfTiles = numberOfTiles;

        Tiles = new Tile[numberOfTiles*numberOfTiles];
        for (int index = 0; index < Tiles.Length; index++)
        {
            Tiles[index] = new Tile(transform.position.y);
            Tiles[index].TileIndex = index;
        }
    }
    public void UpdateEntities()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                Tile t = ConvertStringToTile(transform.GetChild(i).GetChild(j).name);
                t.EntityIndex = 1;
            }
        }
    }
    
    public void UpdateNeighbours()
    {
        for (int index = 0; index < Tiles.Length; index++)
        {
       
            Tile t = Tiles[index];
            t.TileIndex = index;
            Vector2 pos = GetPointInCellFromTileIndex(index);

            //1st Neighbor -- RIGHT
            if (pos.y == _numberOfTiles - 1)
            {
                t.AddNeighbour("None");
            }
            else
            {
                t.AddNeighbour("(" + pos.x + "," + (pos.y + 1) + ")");
            }
            //2nd Neighbor -- UP
            if (pos.x == 0)
            {
                t.AddNeighbour("None");
            }
            else
            {
                t.AddNeighbour("(" + (pos.x - 1) + "," + pos.y + ")");
            }
            //3rd Neighbor -- LEFT
            if (pos.y == 0)
            {
                t.AddNeighbour("None");
            }
            else
            {
                t.AddNeighbour("(" + pos.x + "," + (pos.y - 1) + ")");
            }
            //4th Neighbor -- DOWN
            if (pos.x == _numberOfTiles - 1)
            {
                t.AddNeighbour("None");
            }
            else
            {
                t.AddNeighbour("(" + (pos.x + 1) + "," + pos.y + ")");
            }

        }
    }
    public Tile ConvertStringToTile(string str)
    {
        string [] split = str.Split(',');

        if (split.Length != 2) throw new UnityException("ERROR: Plane has been renamed  --> produced by Toni");

        int x = int.Parse(split[0].Substring(1, split[0].Length - 1));
        int y = int.Parse(split[1].Substring(0, split[1].Length - 1));
        
        return GetTileFromPointInCell(x, y);

    }
    public void UpdateHeight(int x, int y, float height)
    {
        int index = y * _numberOfTiles + x;

        Tiles[index].Heights = height;

        var entity = Tiles[index].Entity;
        if (entity != null)
        {
            var pos = entity.transform.localPosition;
            pos.y = height;
            entity.transform.localPosition = pos;
        }
    }

    public Tile GetTileFromPointInCell(int x, int y)
    {
        int index = y*_numberOfTiles + x;

        if (index >= Tiles.Length || index < 0 ) throw new Exception("ERROR: Tile out of range --> produced by Toni");

        return Tiles[index];
    }

    public Vector2 GetPointInCellFromTileIndex(int tileIndex)
    {
        int index = tileIndex;

        if (index >= Tiles.Length) throw new Exception("ERROR: Tile out of range  --> produced by Toni");

        int x = (index) % _numberOfTiles;
        int y = (index) / _numberOfTiles;

        //Debug.Log("coooords [" + x + "," + y + "]"); //DEBUG
        return new Vector2(x, y);
    }
}