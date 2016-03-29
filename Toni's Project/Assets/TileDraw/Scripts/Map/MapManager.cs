using System;
using System.Collections.Generic;
using TileDraw;
using TileDraw.Map;
using UnityEngine;

[Serializable]
public class MapManager : MonoBehaviour
{
    public List<Cell> Cells = new List<Cell>();
    public string DefaultShader = "Diffuse";

    public TileSet TileTextureSet;
    public EntitySet EntitySet;

    public int TextureResolution = 1024;
    public int NumberOfTiles = 32;
    public float SizeOfCell = 16;

    public float HalfSizeOfCell
    {
        get { return SizeOfCell / 2f; }
    }

    public int TileResolution
    {
        get { return TextureResolution/NumberOfTiles; }
    }

    public float TileSize
    {
        get { return SizeOfCell/NumberOfTiles; }
    }

    public float HalfTileSize
    {
        get { return TileSize/2f; }
    }

    public void RebuildTextureFromTileSet()
    {
        var colors = new Color[TileTextureSet.Assets.Count][];
        for (var index = 0; index < TileTextureSet.Assets.Count; index++)
        {
            colors[index] = Util.ResizeArray(TileTextureSet.Assets[index].GetAsset().GetPixels(), TileResolution);
        }

        foreach (var cell in Cells)
        {
            var texture = (Texture2D) cell.GetComponent<Renderer>().sharedMaterial.mainTexture;

            var index = 0;
            foreach (var t in cell.Tiles)
            {
                if (t.TextureIndex >= 0 && t.TextureIndex < colors.Length)
                {
                    var color = colors[t.TextureIndex];

                    var x = (index%NumberOfTiles)*TileResolution;
                    var y = (index/NumberOfTiles)*TileResolution;

                    texture.SetPixels(x, y, TileResolution, TileResolution, color);
                }

                index++;
            }

            texture.Apply();
        }
    }

    public Tile GetTile(Vector2 point)
    {
        var cell = GetCellOrCreate(point);

        // pointInCell is the point in the cell you want to draw
        var pointInCell = WorldToLocalTile(point);

        return cell.GetTileFromPointInCell((int)pointInCell.x, (int)pointInCell.y);
    }
    public Cell GetCellOrCreate(Vector2 point)
    {
        // cellIndex is the index of the cell
        var cellIndex = point;
        cellIndex.x = Util.RoundTo(cellIndex.x + 0.5f, NumberOfTiles);
        cellIndex.y = Util.RoundTo(cellIndex.y + 0.5f, NumberOfTiles);

        cellIndex = cellIndex / NumberOfTiles;
        var groupName = "(" + cellIndex.x + "," + cellIndex.y + ")";

        var cell = Cells.Find(element => element.name == groupName);

        // If it doesn't exist time to create it
        if (cell == null)
        {
            cell = CellFactory.Create(cellIndex, this);
            Cells.Add(cell);
        }

        return cell;
    }
    /// <summary>
    /// Converts a position in the world to tile coords
    /// </summary>
    /// <param name="world">Point in the world</param>
    /// <returns>World tile coords</returns>
    public Vector2 PositionToWorldTile(Vector3 world)
    {
        var point = new Vector2(world.x, world.z);

        point *= NumberOfTiles / SizeOfCell;

        point.x = Util.RoundTo(point.x, 1);
        point.y = Util.RoundTo(point.y, 1);

        return point;
    }
    /// <summary>
    /// Converts world tile coords to local tiles coords in relavent cell 
    /// </summary>
    /// <param name="point">World tile coords</param>
    /// <returns>Local tile coords</returns>
    public Vector2 WorldToLocalTile(Vector2 point)
    {
        var pointInCell = point;
        pointInCell.x = Util.RoundTo(pointInCell.x + NumberOfTiles / 2f, 1);
        pointInCell.y = Util.RoundTo(pointInCell.y + NumberOfTiles / 2f, 1);

        pointInCell.x = pointInCell.x % NumberOfTiles;
        pointInCell.y = pointInCell.y % NumberOfTiles;

        // Need to invert negative numbers
        if (pointInCell.x < 0) pointInCell.x = NumberOfTiles + pointInCell.x;
        if (pointInCell.y < 0) pointInCell.y = NumberOfTiles + pointInCell.y;

        return pointInCell;
    }
    /// <summary>
    /// Finds the local position of a tile anchored to the middle relative to the cell
    /// </summary>
    /// <param name="point">Local tile position</param>
    /// <returns>Local position</returns>
    public Vector2 LocalTileToLocalPosition(Vector2 point)
    {
        point.x = point.x * (SizeOfCell / NumberOfTiles) - SizeOfCell / 2f + HalfTileSize;
        point.y = point.y * (SizeOfCell / NumberOfTiles) - SizeOfCell / 2f + HalfTileSize;

        return point;
    }

    public Cell GetCell(Vector2 point)
    {
        // cellIndex is the index of the cell
        var cellIndex = point;
        cellIndex.x = Util.RoundTo(cellIndex.x + 0.5f, NumberOfTiles);
        cellIndex.y = Util.RoundTo(cellIndex.y + 0.5f, NumberOfTiles);

        cellIndex = cellIndex / NumberOfTiles;
        var groupName = "(" + cellIndex.x + "," + cellIndex.y + ")";

        // Find the cell
        return Cells.Find(element => element.name == groupName);
    }

    public List<Cell> GetNeighbours(Cell c)
    {
        var neighbours = new [] {new Vector2(-1, -1), 
                                 new Vector2(-1, 0), 
                                 new Vector2(-1, 1), 
                                 new Vector2(0, -1), 
                                 new Vector2(0, 1),
                                 new Vector2(1, -1),
                                 new Vector2(1, 0),
                                 new Vector2(1, 1)};

        var cells = new List<Cell>();

        foreach (var n in neighbours)
        {
            var cell = GetNeighbour(c, (int)n.x, (int)n.y);
            if (cell != null)
                cells.Add(cell);
        }

        return cells;
    }
    public Cell GetNeighbour(Cell c, int xDir, int yDir)
    {
        var split = c.name.Split(',');

        if (split.Length != 2) throw new UnityException("Plane has been renamed");

        var x = int.Parse(split[0].Substring(1, split[0].Length - 1));
        var y = int.Parse(split[1].Substring(0, split[1].Length - 1));

        var cellToFind = "(" + (x + xDir) + "," + (y + yDir) + ")";

        // Find the cell
        var cell = Cells.Find(element => element.name == cellToFind);

        return cell;
    }

#if UNITY_EDITOR
    public bool IsDrawGizmo = true;
    public void OnDrawGizmosSelected()
    {
        if (!IsDrawGizmo) return;

        var range = SizeOfCell * 4;

        var point = new Vector2(Screen.width / 2f, Screen.height / 2f);
        var ray = Camera.current.ScreenPointToRay(point);
        var layerMask = 1 << LayerMask.NameToLayer("LevelTerrain");

        var hits = Physics.RaycastAll(ray, range, layerMask);

        foreach (var hit in hits)
        {
            var mainCell = hit.transform.GetComponent<Cell>();

            if (!Cells.Contains(mainCell)) continue;

            DrawCellGrid(mainCell);

            if (NumberOfTiles < 32)
            {
                var cells = GetNeighbours(mainCell);

                foreach (var cell in cells)
                {
                    DrawCellGrid(cell);
                }
            }

            break;
        }
    }

    private void DrawCellGrid(Cell cell)
    {
        if (!cell.GetComponent<Renderer>().isVisible) return; // if the renderer isn't visible continue

        var mesh = cell.GetComponent<MeshFilter>().sharedMesh;

        var numOfVerts = NumberOfTiles + 1;

        var color = new Color(1, 1, 1, 0.5f);
        Gizmos.color = color;

        for (var j = 0; j < numOfVerts; j++)
        {
            for (var i = 0; i < numOfVerts; i++)
            {
                var z0 = mesh.vertices[j * numOfVerts + i] + cell.transform.position;
                z0.y += 0.01f;

                if (i < NumberOfTiles)
                {
                    var x0 = mesh.vertices[j * numOfVerts + i + 1] + cell.transform.position;
                    x0.y += 0.01f;
                    Gizmos.DrawLine(z0, x0);
                }

                if (j < NumberOfTiles)
                {
                    var y0 = mesh.vertices[(j + 1) * numOfVerts + i] + cell.transform.position;
                    y0.y += 0.01f;
                    Gizmos.DrawLine(z0, y0);
                }
            }
        }
    }
#endif
}