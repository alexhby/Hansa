using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TileDraw
{
    [Serializable]
    public class Entity : IAsset
    {
        [SerializeField]
        private GameObject _gameObject;

        public Entity(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public GameObject GetAsset()
        {
            return _gameObject;
        }

        public void SetAsset(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public Object GetObject()
        {
            return _gameObject;
        }
    }
}