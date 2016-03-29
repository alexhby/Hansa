using System;
using System.Collections.Generic;
using System.Linq;
using TileDraw;

[Serializable]
public class EntitySet : Set
{
    public List<Entity> Assets = new List<Entity>();

    public override List<IAsset> GetAssets()
    {
        return Assets.Cast<IAsset>().ToList();
    }

    public void AddAsset(Entity asset)
    {
        var exists = Assets.Any(t => t.GetAsset() == asset.GetAsset());

        if (!exists)
            Assets.Add(asset);
    }

    public void RemoveAsset(Entity asset)
    {
        Assets.Remove(asset);
    }
}