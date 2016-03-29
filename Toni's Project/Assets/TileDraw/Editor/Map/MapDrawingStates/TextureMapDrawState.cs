using System;
using System.Collections.Generic;
using TileDraw;
using UnityEditor;
using UnityEngine;

namespace LevelEditor
{
    class TextureMapDrawState : MapDrawState
    {
        //private Tile _currentTile;
        private int _currentTileIndex;
        private Color[] _currentTileColors;

        private readonly Texture2D _xTexture;

        private readonly HashSet<Texture2D> _textureUpdate = new HashSet<Texture2D>();

        public TextureMapDrawState(MapManager map) : base(map)
        {
            SetTile(-1);

            _xTexture = AssetDatabase.LoadAssetAtPath(@"Assets/TileDraw/Editor/Map/x.png", typeof(Texture2D)) as Texture2D;
        }

        public override void DrawPreviews()
        {
            TileSet ts = Map.TileTextureSet;
            if (ts == null) return;

            GUI.color = _currentTileIndex == -1 ? Color.white : new Color(0.5f, 0.5f, 0.5f, IconAlpha);
            if (GUILayout.Button(_xTexture, BlankStyle, GUILayout.Width(PreviewSize), GUILayout.Height(PreviewSize)))
            {
                SetTile(-1);
            }
            GUI.color = Color.white;

            try
            {
                for (int index = 0; index < ts.Assets.Count; index++)
                {
                    TextureAsset tileTexture = ts.Assets[index];
#if UNITY_3_5
                    var preview = EditorUtility.GetAssetPreview(tileTexture.GetAsset());
#else
                    var preview = AssetPreview.GetAssetPreview(tileTexture.GetAsset());
#endif
                    if (preview != null)
                    {
                        GUI.color = _currentTileIndex == index ? Color.white : new Color(0.5f, 0.5f, 0.5f, IconAlpha);
                        if (GUILayout.Button(preview, BlankStyle, GUILayout.Width(PreviewSize), GUILayout.Height(PreviewSize)))
                        {
                            SetTile(index);
                        }
                        GUI.color = Color.white;
                    }
                }
            }
            catch (ArgumentException ae)
            {
                // just a rare annoying Unity GUI issue about resizing that occasionally shows but doesn't cause any issues
                Debug.Log(ae);
            }
        }

        public override void DrawAtPoint(Cell cell, Vector2 point)
        {
            var pointInCell = Map.WorldToLocalTile(point);

            cell.Tiles[(int)pointInCell.y * Map.NumberOfTiles + (int)pointInCell.x].TextureIndex = _currentTileIndex;

            var texture = (Texture2D)cell.GetComponent<Renderer>().sharedMaterial.mainTexture;

            // pTexture is point in texture coords
            Vector2 pTexture = pointInCell * Map.TileResolution;
            texture.SetPixels((int)pTexture.x, (int)pTexture.y, Map.TileResolution, Map.TileResolution, _currentTileColors);

            // For performance, don't update right away instead do them all at once at the end
            _textureUpdate.Add(texture);
        }

        public override void FinalizeDraw()
        {
            foreach (var t in _textureUpdate)
            {
                t.Apply();
            }
            _textureUpdate.Clear();
        }

        private void SetTile(int index)
        {
            Color[] colors;

            if (index != -1)
            {
                TextureAsset tileTexture = Map.TileTextureSet.Assets[index];
                colors = tileTexture.GetAsset().GetPixels();
            }
            else
            {
                //_currentTile = null;
                colors = new[] {new Color(0, 0, 0, 0)};
            }

            _currentTileIndex = index;
            _currentTileColors = Util.ResizeArray(colors, Map.TileResolution);
        }
    }
}
