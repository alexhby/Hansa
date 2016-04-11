using System.Linq;
using LevelEditor;
using UnityEditor;
using UnityEngine;

public class MapDrawing : EditorWindow
{
    private MapDrawState _state;

    private Tool _beforeTool;
    private Vector2 _lastPoint;

    private int _brushSize;
    private Vector2 _scroll;

    public MapManager Map;

    public void Init(MapManager map, MapDrawState state = null)
    {
        minSize = new Vector2(84, 196);
        maxSize = new Vector2(84, 800);

        SetState(state);

        Map = map;

        Repaint();
    }

    public void SetState(MapDrawState state)
    {
        _state = state;
    }

    public void OnEnable()
    {
        _beforeTool = Tools.current;
    }

    public void OnDisable()
    {
        Tools.current = _beforeTool;
    }

    public void OnGUI()
    {
        _scroll = GUILayout.BeginScrollView(_scroll, false, true);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Brush");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("<", EditorStyles.miniButtonLeft, GUILayout.Width(20)))
        {
            _brushSize = Mathf.Clamp(_brushSize - 1, 0, 9);
        }
        GUILayout.Box(_brushSize.ToString("0"), EditorStyles.miniButtonMid, GUILayout.Width(20));
        if (GUILayout.Button(">", EditorStyles.miniButtonRight, GUILayout.Width(20)))
        {
            _brushSize = Mathf.Clamp(_brushSize + 1, 0, 9);
        }
        GUI.color = Color.white;

        GUILayout.EndHorizontal();

        if (_state == null) return;

        _state.DrawPreviews();

        GUILayout.EndScrollView();
    }

    // Window has been selected
    public void OnFocus()
    {
        // Remove delegate listener if it has previously been assigned.
        SceneView.onSceneGUIDelegate -= OnSceneGUI;

        // Add (or re-add) the delegate.
        SceneView.onSceneGUIDelegate += OnSceneGUI;
    }

    public void OnDestroy()
    {
        // When the window is destroyed, remove the delegate
        // so that it will no longer do any drawing.
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        Tools.current = Tool.None;

        if (_state == null) return;

        var e = Event.current;

        switch (e.type)
        {
            case EventType.MouseDrag:
            case EventType.MouseDown:
                if (Event.current.button == 0)
                {
                    Ray terrainRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

                    // We raycast till we hit something
                    var layerMask = 1 << LayerMask.NameToLayer("LevelTerrain");

                    RaycastHit[] rayCastHits = Physics.RaycastAll(terrainRay, float.PositiveInfinity, layerMask).OrderBy(h => h.distance).ToArray();

                    // If we don't hit anything
                    if (rayCastHits.Length == 0)
                    {
                        var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                        RayCastCell(ray);
                    }
                    else
                    {
                        if (rayCastHits.Any(h => h.transform.root == Map.transform))
                        {
                            var hit = rayCastHits[0];

                            if (hit.transform.root == Map.transform)
                            {
                                var hitPoint = hit.point;

                                // If we hit on the flat a straight raycast down would miss the plane so we just increase the y slightly
                                hitPoint.y += 0.01f;
                                var ray = new Ray(hitPoint, Vector3.down);
                                RayCastCell(ray);
                            }
                        }
                        else
                        {
                            if (rayCastHits[0].transform.position.y < Map.transform.position.y)
                            {
                                var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                                RayCastCell(ray);
                            }
                        }
                    }
                }
                break;
            case EventType.MouseUp:
                if (Event.current.button == 0)
                {
                    // Reset last point
                    _lastPoint = new Vector3(0.1f, 0.1f, 0.1f);
                }
                break;
            case EventType.Layout:
                HandleUtility.AddDefaultControl(GUIUtility.GetControlID(GetHashCode(), FocusType.Passive));
                break;
            case EventType.Repaint:
                EditorGUIUtility.AddCursorRect(new Rect(0, 0, Screen.width, Screen.height), MouseCursor.Link);
                break;
        }
    }
    
    private void RayCastCell(Ray ray)
    {
        var plane = new Plane(Vector3.up, new Vector3(0, Map.transform.position.y, 0));

        float f;
        // this works if it's flat
        if (plane.Raycast(ray, out f))
        {
            GetPoint(ray, f);
        }
        // this works if it's steep
        else
        {
            var dir = ray.direction;
            dir.y = -dir.y;
            ray.direction = dir;

            if (plane.Raycast(ray, out f))
            {
                GetPoint(ray, f);
            }
        }
    }

    private void GetPoint(Ray ray, float f)
    {
        var point = Map.PositionToWorldTile(ray.GetPoint(f));

        if (point != _lastPoint)
        {
            DrawToPoint(point);

            _lastPoint = point;
        }
    }

    private void DrawToPoint(Vector2 tilePoint)
    {
        for (var y = tilePoint.y - _brushSize; y <= tilePoint.y + _brushSize; y++)
        {
            for (var x = tilePoint.x - _brushSize; x <= tilePoint.x + _brushSize; x++)
            {
                var p = new Vector2(x, y);

                var cell = Map.GetCellOrCreate(p);
                EditorUtility.SetSelectedWireframeHidden(cell.GetComponent<Renderer>(), true);
                _state.DrawAtPoint(cell, p);
            }
        }

        _state.FinalizeDraw();
    }

}