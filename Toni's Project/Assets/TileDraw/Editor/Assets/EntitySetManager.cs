using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using TileDraw;

[Serializable]
public class EntitySetManager : ResourceManager
{
    public override void Init()
    {
        Path += @"EntitySet/";
    }

    protected override Set LoadSet(string file)
    {
        var asset = AssetDatabase.LoadAssetAtPath(file, typeof(EntitySet)) as Set;

        return asset;
    }

    protected override Set CreateSet()
    {
        return CreateInstance(typeof(EntitySet)) as Set;
    }

    protected override Object GetAssetField(Rect rect, Object obj)
    {
        rect.width = 128;
        return EditorGUI.ObjectField(rect, obj, typeof(GameObject), false);
    }

    protected override void AddNewAsset(Object o)
    {
        var t = new Entity((GameObject)o);

        ((EntitySet)CurrentSet).AddAsset(t);

    }

    protected override void RemoveAsset(IAsset o)
    {
        var t = (Entity)o;

        ((EntitySet)CurrentSet).RemoveAsset(t);
    }

    protected override void ShiftLeft(int index)
    {
        var assets = ((EntitySet)CurrentSet).Assets;

        if (index <= 0 || index >= assets.Count) return;

        var a = assets[index];
        assets[index] = assets[index - 1];
        assets[index - 1] = a;
    }
    protected override void ShiftRight(int index)
    {
        var assets = ((EntitySet)CurrentSet).Assets;

        if (index < 0 || index >= assets.Count - 1) return;

        var a = assets[index];
        assets[index] = assets[index + 1];
        assets[index + 1] = a;
    }

}
