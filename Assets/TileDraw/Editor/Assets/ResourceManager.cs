using System;
using System.Collections.Generic;
using System.IO;
using TileDraw;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public abstract class ResourceManager : EditorWindow
{
    protected string Path = @"Assets/TileDraw/Resources/";
    protected string Name = string.Empty;
    protected string Title = string.Empty;
    protected const int Height = 20;
    protected Set CurrentSet;

    private Vector2 _scroll;
    private GUIStyle _guiNoMargin;

    private Object _obj;

    public abstract void Init();
    protected abstract Set LoadSet(string file);
    protected abstract Set CreateSet();
    protected abstract Object GetAssetField(Rect rect, Object obj);
    protected abstract void AddNewAsset(Object o);
    protected abstract void RemoveAsset(IAsset o);
    protected abstract void ShiftLeft(int index);
    protected abstract void ShiftRight(int index);

    private GUIStyle ButtonStyle
    {
        get
        {
            if (_guiNoMargin == null || _guiNoMargin == GUIStyle.none)
            {
                _guiNoMargin = new GUIStyle(GUI.skin.button)
                                   {
                                       margin = new RectOffset(0, 0, 0, 0),
                                       padding = new RectOffset(0, 0, 0, 0)
                                   };
            }
            return _guiNoMargin;
        }
    }
    
    protected virtual void Options() { }
    public virtual void OnInspectorUpdate()
    {
        Repaint();
    }

    public void OnGUI()
    {
        EditorGUILayout.BeginVertical(GUILayout.Height(60));
        GUILayout.FlexibleSpace();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("Set Name: ", GUILayout.Width(100));
        Name = EditorGUILayout.TextField(Name, GUILayout.Width(100));
        if (GUILayout.Button("Create"))
        {
            if (FileNameCheck(Name))
            {
                if (!File.Exists(Path + Name + ".asset"))
                {
                    var t = CreateSet();
                    t.name = Name;

                    AssetDatabase.CreateAsset(t, Path + Name + ".asset");

                    Name = string.Empty;
                    GUIUtility.keyboardControl = 0;

                    Repaint();
                }
            }

        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndVertical();

        GUI.color = Color.black;
        GUI.Box(new Rect(0, 65, position.width, 1), new GUIContent());
        GUI.color = Color.white;

        LeftColumn(70, 150);
        GUI.color = Color.black;
        GUI.Box(new Rect(155, 65, 2, position.height - 65), new GUIContent());
        GUI.color = Color.white;
        RightColumn(160, 70, position.width - 160);
    }

    private void LeftColumn(float yOffset, float width)
    {
        EditorGUI.LabelField(new Rect(0, yOffset, width, Height), "Sets");
        yOffset += Height;

        if (!Directory.Exists(Path))
            Directory.CreateDirectory(Path);
        var files = Directory.GetFiles(Path);

        for (var index = 0; index < files.Length; index++)
        {
            var file = files[index];

            var set = LoadSet(file);
            if (set != null)
            {
                if (CurrentSet == set)
                {
                    GUI.color = Color.cyan;
                }

                var rect = new Rect(0, yOffset, width - Height, Height);
                GUI.Box(rect, set.name);
                GUI.color = Color.red;
                var rectX = new Rect(width - Height, yOffset, Height, Height);
                if (GUI.Button(rectX, "X"))
                {
                    if (EditorUtility.DisplayDialog("Delete EntitySet?",
                                                    "Are you sure you want to delete " + set.name + "?",
                                                    "Delete", "Cancel"))
                    {
                        AssetDatabase.DeleteAsset(file);
                        files[index].Remove(index);
                        index--;
                    }
                }

                yOffset += Height;

                if (Event.current.type == EventType.MouseDown)
                {
                    if (rect.Contains(Event.current.mousePosition))
                    {
                        if (Event.current.button == 0)
                        {
                            CurrentSet = set;
                        }
                        if (Event.current.button == 1)
                        {

                            index--;
                        }
                    }
                }
                GUI.color = Color.white;
            }
        }
    }

    private void RightColumn(float xOffset, float yOffset, float width)
    {
        const int topheight = 196;

        SetObjects(xOffset, yOffset, width, topheight);
        GUI.color = Color.black;
        GUI.Box(new Rect(xOffset - 5, yOffset + topheight, width + 5, 1), new GUIContent());
        GUI.color = Color.white;
        SetOptions(xOffset, yOffset + topheight + 4);
    }

    private void SetObjects(float xOffset, float yOffset, float width, float height)
    {
        if (CurrentSet == null) return;

        List<IAsset> assets = CurrentSet.GetAssets();
        int widthMinusBar = (int)width - 20;

        var iconPerLevel = Mathf.Max(widthMinusBar / 64, 1);
        var levels = assets.Count/iconPerLevel + assets.Count%iconPerLevel > 0 ? 1 : 0;

        var h = Math.Max(levels*64, height);

        _scroll = GUI.BeginScrollView(new Rect(xOffset, yOffset, width, height), _scroll, new Rect(0, 0, widthMinusBar, h), false, true);

        var currentOffset = 0;
        var currentWidth = 0;

        var yOffsetScroll = 0;

        for (int index = 0; index < CurrentSet.GetAssets().Count; index++)
        {
            var obj = assets[index];

#if UNITY_3_5
            var preview = EditorUtility.GetAssetPreview(obj.GetObject());
#else
            var preview = AssetPreview.GetAssetPreview(obj.GetObject());
#endif
            if (preview != null)
            {
                currentWidth += 64;
                if (currentWidth > widthMinusBar)
                {
                    currentOffset = 0;
                    yOffsetScroll += 64;
                    currentWidth = 64;
                }

                var rect = new Rect(currentOffset, yOffsetScroll, 64, 64);
                EditorGUI.DrawPreviewTexture(rect, preview);

                GUI.color = Color.red;
                if (GUI.Button(new Rect(currentOffset + 64 - 16, yOffsetScroll, 16, 16), "X", ButtonStyle))
                {
                    if (EditorUtility.DisplayDialog("Delete EntitySet?",
                                                    "Are you sure you want to delete entity??",
                                                    "Delete", "Cancel"))
                    {

                        RemoveAsset(obj);
                        index--;
                    }
                }
                GUI.color = Color.white;

                GUI.enabled = index != assets.Count - 1;
                if (GUI.Button(new Rect(currentOffset + 64 - 16, yOffsetScroll + 16, 16, 16), ">", ButtonStyle))
                {
                    ShiftRight(index);
                }
                GUI.enabled = index != 0;
                if (GUI.Button(new Rect(currentOffset + 64 - 16, yOffsetScroll + 32, 16, 16), "<", ButtonStyle))
                {
                    if (index != 0)
                    {
                        ShiftLeft(index);
                    }
                }
                GUI.enabled = true;

                currentOffset += 64;
            }
        }

        GUI.EndScrollView();
    }


    private void SetOptions(float xOffset, float yOffset)
    {
        if (CurrentSet == null) return;

        float xOffsetC = xOffset;

        var rect = new Rect(xOffsetC, yOffset, 64, Height);
        GUI.Label(rect, "Add:");
        xOffsetC += 64;

        rect = new Rect(xOffsetC, yOffset, 64, Height);
        xOffsetC += 128;

        _obj = GetAssetField(rect, _obj);

        var buttonRect = new Rect(xOffsetC, yOffset, 64, Height);

        if (GUI.Button(buttonRect, "Add") && _obj != null)
        {
            AddNewAsset(_obj);
            EditorUtility.SetDirty(CurrentSet);
            _obj = null;
        }
    }


    private static bool FileNameCheck(string entityName)
    {
        if (entityName == string.Empty) return false;
        if (entityName == null) return false;

        return true;
    }
}