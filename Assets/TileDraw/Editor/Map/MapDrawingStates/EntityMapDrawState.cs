using System;
using System.Globalization;
using TileDraw;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LevelEditor
{
    class EntityMapDrawState : MapDrawState
    {
        private Entity _currentEntity;
        private int _currentEntityIndex;
        private readonly Texture2D _xTexture;

        public EntityMapDrawState(MapManager map) : base(map)
        {
            SetEntity(-1);

            _xTexture = AssetDatabase.LoadAssetAtPath(@"Assets/TileDraw/Editor/Map/x.png", typeof(Texture2D)) as Texture2D;
        }

        public override void DrawPreviews()
        {
            var es = Map.EntitySet;
            if (es == null) return;

            GUI.color = _currentEntityIndex == -1 ? Color.white : new Color(1, 1, 1, IconAlpha);
            if (GUILayout.Button(_xTexture, BlankStyle, GUILayout.Width(PreviewSize), GUILayout.Height(PreviewSize)))
            {
                SetEntity(-1);
            }
            GUI.color = Color.white;

            try
            {
                for (int index = 0; index < es.Assets.Count; index++)
                {
                    var entity = es.Assets[index];
#if UNITY_3_5
                    var preview = EditorUtility.GetAssetPreview(entity.GetAsset());
#else
                    var preview = AssetPreview.GetAssetPreview(entity.GetAsset());
#endif
                    if (preview != null)
                    {
                        GUI.color = _currentEntityIndex == index ? Color.white : new Color(1, 1, 1, IconAlpha);
                        if (GUILayout.Button(preview, BlankStyle, GUILayout.Width(PreviewSize), GUILayout.Height(PreviewSize)))
                        {
                            SetEntity(index);
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
            // location of entity
            var pointInCell = Map.WorldToLocalTile(point);

            var tile = cell.GetTileFromPointInCell((int)pointInCell.x, (int)pointInCell.y);

            if (tile.EntityIndex != _currentEntityIndex)
            {
                tile.EntityIndex = _currentEntityIndex;

                // Remove old if an object was there already
                var old = tile.Entity;
                Object.DestroyImmediate(old);

                if (_currentEntity != null)
                {
                    var entity = (GameObject) PrefabUtility.InstantiatePrefab(_currentEntity.GetAsset());
                    entity.name = "(" + pointInCell.x + "," + pointInCell.y + ")";
                    entity.isStatic = true;

                    var parentName = _currentEntityIndex.ToString(CultureInfo.InvariantCulture) + " " +
                                     _currentEntity.GetAsset().name;
                    var parent = cell.transform.Find(parentName);
                    if (parent == null)
                    {
                        var pgo = new GameObject(parentName);
                        parent = pgo.transform;
                        parent.parent = cell.transform;
                        parent.transform.localPosition = Vector3.zero;
                    }

                    var position = Map.LocalTileToLocalPosition(pointInCell);
                    entity.transform.parent = parent;
                    entity.transform.localPosition = new Vector3(position.x, tile.Heights, position.y);

                    tile.Entity = entity;

                    EditorUtility.SetSelectedWireframeHidden(entity.GetComponent<Renderer>(), true);
                }
            }
        }

        private void SetEntity(int index)
        {
            _currentEntityIndex = index;

            _currentEntity = index == -1 ? null : Map.EntitySet.Assets[index];
        }
    }
}
