using System.Collections.Generic;
using UnityEngine;

namespace TileDraw.Map
{
    public class MapEditing
    {
        private delegate void HandleEdge(Cell c, int x, int y, int xDir, int yDir);

        private readonly MapManager _map;

        //private readonly HashSet<Cell> _collidersToUpdate = new HashSet<Cell>();
        private readonly Dictionary<Cell, Vector3[]> _verts = new Dictionary<Cell, Vector3[]>();
        
        public MapEditing(MapManager map)
        {
            _map = map;
        }

        public void SetTileHeight(Cell cell, Vector2 pointInCell, float height)
        {
            CheckMeshInDictionary(cell);

            var count = _map.NumberOfTiles + 1;
            //var arrayIndex = (int)pointInCell.y * _map.NumberOfTiles + (int)pointInCell.x;
            var ix = (int)pointInCell.x;
            var iy = (int)pointInCell.y;

            //cell.UpdateHeight(ix, iy, height);

            for (var y = iy; y <= iy + 1; y++)
            {
                for (var x = ix; x <= ix + 1; x++)
                {
                    var index = y * count + x;

                    _verts[cell][index].y = height;

                    CheckOnEdge(cell, x, y, UpdateNeighbourHeight);
                }
            }
        }

        public void UpdateCellFromNeighbours(Cell c)
        {
            for (var x = 0; x < _map.NumberOfTiles+1; x++)
            {
                CheckOnEdge(c, x, 0, UpdateOwnHeight);
                CheckOnEdge(c, x, _map.NumberOfTiles, UpdateOwnHeight);
            }

            for (var y = 1; y < _map.NumberOfTiles; y++)
            {
                CheckOnEdge(c, 0, y, UpdateOwnHeight);
                CheckOnEdge(c, _map.NumberOfTiles, y, UpdateOwnHeight);
            }
        }

        private void CheckOnEdge(Cell cell, int x, int y, HandleEdge workOnEdge)
        {
            var neighbours = Neighbour.None;

            if (x == 0) neighbours |= Neighbour.Left;
            if (x == _map.NumberOfTiles) neighbours |= Neighbour.Right;
            if (y == 0) neighbours |= Neighbour.Top;
            if (y == _map.NumberOfTiles) neighbours |= Neighbour.Bottom;

            if ((neighbours & Neighbour.Left) == Neighbour.Left)
            {
                workOnEdge(cell, x, y, -1, 0);
            }
            if ((neighbours & Neighbour.Top) == Neighbour.Top)
            {
                workOnEdge(cell, x, y, 0, -1);
            }
            if ((neighbours & Neighbour.Right) == Neighbour.Right)
            {
                workOnEdge(cell, x, y, 1, 0);
            }
            if ((neighbours & Neighbour.Bottom) == Neighbour.Bottom)
            {
                workOnEdge(cell, x, y, 0, 1);
            }
            if ((neighbours & Neighbour.LeftTop) == Neighbour.LeftTop)
            {
                workOnEdge(cell, x, y, -1, -1);
            }
            if ((neighbours & Neighbour.RightTop) == Neighbour.RightTop)
            {
                workOnEdge(cell, x, y, 1, -1);
            }
            if ((neighbours & Neighbour.LeftBottom) == Neighbour.LeftBottom)
            {
                workOnEdge(cell, x, y, -1, 1);
            }
            if ((neighbours & Neighbour.RightBottom) == Neighbour.RightBottom)
            {
                workOnEdge(cell, x, y, 1, 1);
            }
        }

        private void UpdateNeighbourHeight(Cell cell, int x, int y, int xDir, int yDir)
        {
            var count = _map.NumberOfTiles + 1;
            var neighbour = _map.GetNeighbour(cell, xDir, yDir);

            if (neighbour != null)
            {
                var yy = (yDir == -1) ? _map.NumberOfTiles :
                         (yDir == 1) ? 0 : y;

                var xx = (xDir == -1) ? _map.NumberOfTiles :
                         (xDir == 1) ? 0 : x;

                // get height of current cell
                var height = _verts[cell][y * count+x].y;

                CheckMeshInDictionary(neighbour);
                _verts[neighbour][yy * count + xx].y = height;
            }
        }

        private void UpdateOwnHeight(Cell cell, int x, int y, int xDir, int yDir)
        {
            var neighbour = _map.GetNeighbour(cell, xDir, yDir);

            if (neighbour != null)
            {
                var yy = (yDir == -1) ? _map.NumberOfTiles :
                         (yDir == 1) ? 0 : y;

                var xx = (xDir == -1) ? _map.NumberOfTiles :
                         (xDir == 1) ? 0 : x;

                var count = _map.NumberOfTiles + 1;

                var v = neighbour.GetComponent<MeshFilter>().sharedMesh.vertices;
                var height = v[yy*count + xx].y;

                CheckMeshInDictionary(cell);
                _verts[cell][y*count + x].y = height;
            }
        }

        public void Apply()
        {
            foreach (KeyValuePair<Cell, Vector3[]> pair in _verts)
            {
                var cell = pair.Key;
                var mesh = cell.GetComponent<MeshFilter>().sharedMesh;
                mesh.vertices = pair.Value;

                var mc = cell.GetComponent<MeshCollider>();
                mc.sharedMesh = null;
                mc.sharedMesh = mesh;

                UpdateHeights(pair.Key); // TODO MAKE EFFICIENT
            }
            _verts.Clear();
        }

        private void UpdateHeights(Cell cell)
        {
                var mesh = cell.GetComponent<MeshFilter>().sharedMesh;

                var numOfVerts = _map.NumberOfTiles + 1;

                for (var y = 0; y < _map.NumberOfTiles; y++)
                {
                    for (var x = 0; x < _map.NumberOfTiles; x++)
                    {
                        var vx = mesh.vertices[y * numOfVerts + (x + 1)];
                        var vy = mesh.vertices[(y + 1) * numOfVerts + x];

                        var height = (vx.y + vy.y) / 2f;

                        cell.UpdateHeight(x, y, height);

                    }
                }
        }

        private void CheckMeshInDictionary(Cell cell)
        {
            if (!_verts.ContainsKey(cell))
            {
                var mf = cell.GetComponent<MeshFilter>();
                var mesh = mf.sharedMesh;

                _verts.Add(cell, mesh.vertices);

            }
        }
    }
}
