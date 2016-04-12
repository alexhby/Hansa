using System;
using System.Collections.Generic;
using TileDraw;
using UnityEngine;

[Serializable]
public abstract class Set : ScriptableObject
{
    /// <summary>
    /// Readonly copy 
    /// </summary>
    /// <returns></returns>
    public abstract List<IAsset> GetAssets();
}