using TileDraw.Map;
using LevelEditor;
using UnityEngine;
using UnityEditor;

public class VertexDrawState : MapDrawState
{
    private static float _heightPerLevel = 0.2f;

    private int _heightLevel;
    //private bool _isVertex;

    private readonly MapEditing _mapEditing;


    public VertexDrawState(MapManager map) : base(map)
    {
        _mapEditing = new MapEditing(map);
    }

    public override void DrawPreviews()
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Level");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        _heightPerLevel = EditorGUILayout.FloatField(_heightPerLevel);
        _heightPerLevel = Mathf.Max(_heightPerLevel, 0.1f); // clamp at 0

        if (GUILayout.Button(_heightLevel.ToString("0"), EditorStyles.miniButton, GUILayout.Width(60)))
        {
            _heightLevel = 0;
        }


        GUILayout.BeginHorizontal();
        if (GUILayout.Button("-", EditorStyles.miniButtonLeft, GUILayout.Width(30)))
        {
            _heightLevel--;
        }
        if (GUILayout.Button("+", EditorStyles.miniButtonRight, GUILayout.Width(30)))
        {
            _heightLevel++;
        }

        GUILayout.EndHorizontal();

       /* GUILayout.BeginHorizontal();
        GUI.color = _isVertex ? Color.red : Color.white;
        if (GUILayout.Button("V", EditorStyles.miniButtonLeft))
        {
            _isVertex = true;
        }
        GUI.color = !_isVertex ? Color.red : Color.white;
        if (GUILayout.Button("T", EditorStyles.miniButtonRight))
        {
            _isVertex = false;
        }
        GUI.color = Color.white;
        GUILayout.EndHorizontal();*/
    }

    public override void DrawAtPoint(Cell cell, Vector2 point)
    {
        var pointInCell = Map.WorldToLocalTile(point);
        var height = _heightLevel*_heightPerLevel;

        _mapEditing.SetTileHeight(cell, pointInCell, height);
    }

    public override void FinalizeDraw()
    {
        _mapEditing.Apply();
    }
}
