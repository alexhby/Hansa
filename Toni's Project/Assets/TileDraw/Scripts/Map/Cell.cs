using System;
using TileDraw.Map;
using UnityEngine;

[Serializable]
public class Cell : MonoBehaviour
{
    public Tile[] Tiles;
    [SerializeField] private int _numberOfTiles;

    void Update()
    {


            MapManager mm = new MapManager();
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(Camera.main.transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 2000, Color.magenta, 10);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Collision at [" + hit.point.x + "," + hit.point.z + "]");
                Vector2 point = convertWorldPosToIndex (new Vector2(hit.point.x,hit.point.z));
                Tile t = GetTileFromPointInCell((int)point.x, (int)point.y);
                Debug.Log("1: [" + (int)point.x + "," + (int)point.y + "]");
                Vector2 worldPos = convertIndexToWorldPos(point);
                GameObject project = (GameObject)Instantiate(Resources.Load("Projector") , new Vector3(worldPos.x,1,worldPos.y), Quaternion.Euler(90,0,0) );

            }

    }
    public Vector2 convertIndexToWorldPos(Vector2 index)
    {
        int xPos = (int)index.x;
        int yPos = (int)index.y;
     
        MapManager m = new MapManager();
        xPos = -(m.NumberOfTiles / 2) + (xPos);
        yPos = -(m.NumberOfTiles / 2) + (yPos);
        return new Vector2(xPos, yPos);
    }
    public Vector2 convertWorldPosToIndex(Vector2 index)
    {
        int xPos = (int)Math.Round(index.x);
        int yPos = (int)Math.Round(index.y);
        Debug.Log("2: [" + (int)xPos + "," + yPos + "]");
        MapManager m = new MapManager();
        xPos = xPos + (m.NumberOfTiles / 2);
        yPos = yPos + (m.NumberOfTiles / 2);

        return new Vector2(xPos, yPos);
    }
    public void GenerateTiles(int numberOfTiles)
    {
        _numberOfTiles = numberOfTiles;

        Tiles = new Tile[numberOfTiles*numberOfTiles];
        for (int index = 0; index < Tiles.Length; index++)
        {
            Tiles[index] = new Tile(transform.position.y);
        }
    }

    public void UpdateHeight(int x, int y, float height)
    {
        var index = y * _numberOfTiles + x;

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
        var index = y*_numberOfTiles + x;

        if (index >= Tiles.Length) throw new UnityException("Tile out of range");

        return Tiles[index];
    }
}