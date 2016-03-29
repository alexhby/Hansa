using System;
using System.Collections.Generic;
using UnityEngine;

namespace TileDraw.Map
{
    [Serializable]
    public class Tile
    {
        public float Heights;
        public int TextureIndex; // index relating to the
        public GameObject Entity; // entity that is sitting on the tile
        public int EntityIndex; // 
        public List<Tile> Neighbours = new List<Tile>();

        public Tile(float height)
        {
            Heights = height;
            TextureIndex = -1;
            EntityIndex = -1;
        }

        public void SetNeighbour(Tile tile)
        {

        }
        public void RemoveLink(Tile tile)
        {

        }

    }
}


