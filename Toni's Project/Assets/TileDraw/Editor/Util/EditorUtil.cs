using System.IO;
using UnityEditor;
using UnityEngine;

namespace TileDraw
{
    public static class EditorUtil
    {
        public static void CheckTexture(Texture2D texture)
        {
            var path = AssetDatabase.GetAssetPath(texture);

            if (File.Exists(path))
            {
                var textureImporter = (TextureImporter)AssetImporter.GetAtPath(path);
                if (!textureImporter.isReadable)
                {
                    AssetDatabase.StartAssetEditing();
                    textureImporter.textureType = TextureImporterType.Advanced;
                    textureImporter.isReadable = true;
                    textureImporter.textureFormat = TextureImporterFormat.ARGB32;
                    AssetDatabase.ImportAsset(path);
                    AssetDatabase.Refresh();
                    AssetDatabase.StopAssetEditing();
                }
            }
        }
        
    }
}
