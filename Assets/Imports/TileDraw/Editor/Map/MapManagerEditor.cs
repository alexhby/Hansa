using TileDraw.Map;
using LevelEditor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof (MapManager))]
public class MapManagerEditor : Editor
{
    private bool _entityFoldout;

    public SerializedProperty DefaultShader;
    public SerializedProperty Cells;
    public SerializedProperty TextureSet;
    public SerializedProperty EntitySet;
    public SerializedProperty IsDrawGizmo;

    public void OnEnable()
    {
        // Setup the SerializedProperties
        Cells = serializedObject.FindProperty("Cells");
        DefaultShader = serializedObject.FindProperty("DefaultShader");
        TextureSet = serializedObject.FindProperty("TileTextureSet");
        EntitySet = serializedObject.FindProperty("EntitySet");
        IsDrawGizmo = serializedObject.FindProperty("IsDrawGizmo");

        var mm = (MapManager) target;

        for (int index = 0; index < mm.Cells.Count; index++)
        {
            var cell = mm.Cells[index];
            if (cell == null)
            {
                mm.Cells.Remove(cell);
                index--;
                continue;
            }

            EditorUtility.SetSelectedWireframeHidden(cell.GetComponent<Renderer>(), true);

            foreach (Tile t in cell.Tiles)
            {
                var e = t.Entity;
                if (e != null)
                {
                    EditorUtility.SetSelectedWireframeHidden(e.GetComponent<Renderer>(), true);
                }
                else
                {
                    t.EntityIndex = -1;
                }
            }
        }
    }

    public void OnDisable()
    {
        var mt = EditorWindow.GetWindow<MapDrawing>(true, "");
        mt.Close();

        var mm = (MapManager) target;

        foreach (var cell in mm.Cells)
        {
            if (cell != null)
            {
                EditorUtility.SetSelectedWireframeHidden(cell.GetComponent<Renderer>(), false);

                foreach (Tile t in cell.Tiles)
                {
                    var e = t.Entity;
                    if (e != null)
                    {
                        EditorUtility.SetSelectedWireframeHidden(e.GetComponent<Renderer>(), false);
                    }
                }
            }
        }
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        serializedObject.Update();

        var t = (MapManager) target;
        var p = t.transform.position;
        p.x = 0;
        p.z = 0;
        t.transform.position = p;

        DefaultShader.stringValue = EditorGUILayout.TextField("Default Shader", DefaultShader.stringValue);

        var ts = TextureSet.objectReferenceValue;
        var newts = EditorGUILayout.ObjectField("Tile Set", ts, typeof (TileSet), false);
        if (ts != newts && newts != null)
        {
            TextureSet.objectReferenceValue = newts;
            serializedObject.ApplyModifiedProperties();
            ((MapManager) target).RebuildTextureFromTileSet();
        }

        var es = EntitySet.objectReferenceValue;
        var newes = EditorGUILayout.ObjectField("Entity Set", es, typeof (EntitySet), false);
        if (es != newes)
        {
            EntitySet.objectReferenceValue = newes;
        }

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Draw");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Vertex", EditorStyles.miniButtonMid, GUILayout.Width(100)))
        {
            var md = EditorWindow.GetWindow<MapDrawing>(true, "");
            md.Init(t, new VertexDrawState(t));
        }

        GUI.enabled = TextureSet.objectReferenceValue != null;
        if (GUILayout.Button("Textures", EditorStyles.miniButtonMid, GUILayout.Width(100)))
        {
            var md = EditorWindow.GetWindow<MapDrawing>(true, "");
            md.Init(t, new TextureMapDrawState(t));
        }

        GUI.enabled = EntitySet.objectReferenceValue != null;
        if (GUILayout.Button("Entities", EditorStyles.miniButtonMid, GUILayout.Width(100)))
        {
            var md = EditorWindow.GetWindow<MapDrawing>(true, "");
            md.Init(t, new EntityMapDrawState(t));
        }
        GUI.enabled = true;
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        IsDrawGizmo.boolValue = EditorGUILayout.Toggle("Show Grid (editor only):", IsDrawGizmo.boolValue);

        if (GUILayout.Button("Rebuild Textures", EditorStyles.miniButton, GUILayout.Width(150)))
        {
            t.RebuildTextureFromTileSet();
        }

        serializedObject.ApplyModifiedProperties();
    }
}