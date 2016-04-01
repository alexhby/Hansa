using System;
using System.Collections.Generic;
using System.Linq;
using TileDraw;

[Serializable]
public class TileSet : Set
{
    public List<TextureAsset> Assets = new List<TextureAsset>();

    public override List<IAsset> GetAssets()
    {
        return Assets.Cast<IAsset>().ToList();
    }

    public void AddAsset(TextureAsset asset)
    {
        var exists = Assets.Any(t => t.GetAsset() == asset.GetAsset());

        if (!exists)
            Assets.Add(asset);
    }
    public void RemoveAsset(TextureAsset asset)
    {
        Assets.Remove(asset);
    }
}