using UnityEngine;

namespace TileDraw.Map
{
    public static class CellFactory
    {
        public static Cell Create(Vector2 index, MapManager map)
        {
            var name = "(" + index.x + "," + index.y + ")";

            var go = new GameObject(name);

            var mf = go.AddComponent<MeshFilter>();
            mf.mesh = CreatePlane.Create(name, map.SizeOfCell, map.SizeOfCell, map.NumberOfTiles, map.NumberOfTiles);

            go.AddComponent<MeshRenderer>();
            go.AddComponent<MeshCollider>();

            var cell = go.AddComponent<Cell>();
            cell.GenerateTiles(map.NumberOfTiles);

            go.transform.parent = map.transform;

            // cellPosition is the position in the world of the cell
            var cellPosition = index;
            cellPosition *= map.SizeOfCell;
            cellPosition.x -= map.HalfTileSize;
            cellPosition.y -= map.HalfTileSize;

            go.transform.position = new Vector3(cellPosition.x, map.transform.position.y, cellPosition.y);

            go.layer = LayerMask.NameToLayer("LevelTerrain");

            var t = new Texture2D(map.TileResolution * map.NumberOfTiles, map.TileResolution * map.NumberOfTiles);
            var texName = "TEX" + name;
            t.name = texName;
            var transparent = new Color[map.TileResolution * map.NumberOfTiles * map.TileResolution * map.NumberOfTiles];

            var c = new Color(0, 0, 0, 0);

            for (var y = 0; y < map.TileResolution * map.NumberOfTiles; y++)
            {
                for (var x = 0; x < map.TileResolution * map.NumberOfTiles; x++)
                {
                    transparent[y * map.TileResolution * map.NumberOfTiles + x] = c;
                }
            }

            t.SetPixels(transparent);
            t.wrapMode = TextureWrapMode.Clamp;
            t.Apply();

            var shader = Shader.Find(map.DefaultShader);

            if (shader == null)
            {
                shader = Shader.Find("Diffuse");
            }
            var m = new Material(shader);
            var matName = "MAT" + name;
            m.name = matName;

            m.mainTexture = t;
            go.GetComponent<Renderer>().sharedMaterial = m;

            var me = new MapEditing(map);
            me.UpdateCellFromNeighbours(cell);
            me.Apply();

            return cell;
        }
    }
}
