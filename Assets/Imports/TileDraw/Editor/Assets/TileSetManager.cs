using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using TileDraw;

[Serializable]
public class TileSetManager : ResourceManager
{
    public override void Init()
    {
        Path += @"TileSet/";
    }

    protected override Set LoadSet(string file)
    {
        var asset = AssetDatabase.LoadAssetAtPath(file, typeof(TileSet)) as Set;

        return asset;
    }

    protected override Set CreateSet()
    {
        return CreateInstance(typeof(TileSet)) as Set;
    }

    protected override Object GetAssetField(Rect rect, Object obj)
    {
        rect.height = 64;
        return EditorGUI.ObjectField(rect, obj, typeof(Texture2D), false);
    }

    protected override void AddNewAsset(Object o)
    {
        var texture = (Texture2D) o;
        EditorUtil.CheckTexture(texture);

        var t = new TextureAsset(texture);

        ((TileSet)CurrentSet).AddAsset(t);

    }

    protected override void RemoveAsset(IAsset o)
    {
        var t = (TextureAsset) o;

        ((TileSet)CurrentSet).RemoveAsset(t);
    }

    protected override void ShiftLeft(int index)
    {
        var assets = ((TileSet)CurrentSet).Assets;

        if (index <= 0 || index >= assets.Count) return;

        var a = assets[index];
        assets[index] = assets[index - 1];
        assets[index - 1] = a;
    }
    protected override void ShiftRight(int index)
    {
        var assets = ((TileSet)CurrentSet).Assets;

        if (index < 0 || index >= assets.Count - 1) return;

        var a = assets[index];
        assets[index] = assets[index + 1];
        assets[index + 1] = a;
    }
}
