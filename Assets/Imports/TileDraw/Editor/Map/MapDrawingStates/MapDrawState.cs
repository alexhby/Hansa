using UnityEngine;

namespace LevelEditor
{
    public abstract class MapDrawState
    {
        protected const float PreviewSize = 64;
        protected const float IconAlpha = 0.5f;

        protected static GUIStyle BlankStyle = new GUIStyle();
        
        protected MapManager Map;
        
        protected MapDrawState(MapManager map)
        {
            Map = map;
        }

        public abstract void DrawPreviews();
        public abstract void DrawAtPoint(Cell cell, Vector2 point);
        public virtual void FinalizeDraw() { }
    }
}
