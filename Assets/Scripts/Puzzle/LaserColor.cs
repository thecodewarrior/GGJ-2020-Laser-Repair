using UnityEngine;

namespace Puzzle
{
    public class LaserColor
    {
        public static readonly LaserColor
            NONE = new LaserColor(Color.magenta, Color.white);
        
        public readonly Color DebugColor;
        public readonly Color Color;

        public LaserColor(Color debugColor, Color color)
        {
            DebugColor = debugColor;
            Color = color;
        }
    }
}