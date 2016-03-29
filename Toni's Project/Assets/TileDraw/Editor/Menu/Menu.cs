using UnityEditor;
using UnityEngine;

namespace LevelEditor
{
    public class Menu
    {
        [MenuItem(@"Window/TileDraw/Add a Map Layer", false, 0)]
        public static void MapManager()
        {
            ScriptableWizard.DisplayWizard <MapManagerWizard>("Create Map Layer");
        }

        [MenuItem(@"Window/TileDraw/Resource Managers/Tile Set", false, 11)]
        public static void TileSet()
        {
            var window = EditorWindow.GetWindow<TileSetManager>();
            window.Init();
            window.minSize = new Vector2(416,344);
        }
        [MenuItem(@"Window/TileDraw/Resource Managers/Entity Set", false, 11)]
        public static void EntitySet()
        {
            var window = EditorWindow.GetWindow<EntitySetManager>();
            window.Init();
            window.minSize = new Vector2(416, 344);
        }
    }
}