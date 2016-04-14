using System;
using System.Collections.Generic;
using UnityEngine;

namespace TileDraw.Map
{
    [Serializable]
    public class Tile
    {
        public float Heights;
        public int TextureIndex;
        public int TileIndex { get; set; }
        public int gCost { get; set; } //distance from starting node
        public int hCost { get; set; } //distance from target node

        [NonSerialized] public GameObject Entity;
        [NonSerialized] public Tile parent;
        public int EntityIndex;
        public string EntityString;
        public List<string> Neighbours = new List<string>();

        //constructor
        public Tile(float height)
        {
            Heights = height;
            TextureIndex = -1;
            EntityIndex = -1;
        }
        //getters and setters
        public string GetEntityString()
        {
            return EntityString;
        }
        public void SetEntityString(string str)
        {
            EntityString = str;
        }
        public float GetHeight()
        {
            return Heights;
        }
        //methods
        public void AddNeighbour(string tile)
        {
            Neighbours.Add(tile);
        }
        public void RemoveLink(Tile tile)
        {

        }
        public int fCost
        {
            get
            {
                return gCost + hCost;
            }
        }
    }
}


