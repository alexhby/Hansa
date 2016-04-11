using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TileDraw
{
    [Serializable]
    public class TextureAsset : IAsset
    {
        [SerializeField] private Texture2D _texture;

        public TextureAsset(Texture2D texture)
        {
            _texture = texture;
        }

        public Texture2D GetAsset()
        {
            return _texture;
        }

        public void SetAsset(Texture2D texture)
        {
            _texture = texture;
        }

        public Object GetObject()
        {
            return _texture;
        }
    }
}