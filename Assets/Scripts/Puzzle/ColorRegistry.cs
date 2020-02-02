using System;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    public class ColorRegistry : MonoBehaviour
    {
        public LaserColor[] colors;
        public ColorSplit[] colorSplit;
        public ColorMerge[] colorMerges;
        private Dictionary<string, LaserColor> colorsByName;

        private void Start()
        {
            colorsByName = new Dictionary<string, LaserColor>();
            foreach (var color in colors)
            {
                colorsByName[color.id] = color;
            }
        }

        private static ColorRegistry currentRegistry;

        private static void GetRegistry()
        {
            if (!currentRegistry)
            {
                currentRegistry = FindObjectOfType<ColorRegistry>();
            }
        }
        
        public static LaserColor GetColor(string name)
        {
            GetRegistry();
            if (currentRegistry && currentRegistry.colorsByName.ContainsKey(name.ToLowerInvariant()))
                return currentRegistry.colorsByName[name.ToLowerInvariant()];
            return null;
        }

        public static LaserColor MergeColors(LaserColor colorA, LaserColor colorB)
        {
            GetRegistry();
            if (currentRegistry)
            {
                foreach (var merge in currentRegistry.colorMerges)
                {
                    if ((merge.inputA == colorA && merge.inputB == colorB) ||
                        (merge.inputA == colorB && merge.inputB == colorA))
                        return merge.output;
                }
            }

            return colorA;
        }

        public static (LaserColor, LaserColor) SplitColor(LaserColor color)
        {
            GetRegistry();
            if (currentRegistry)
            {
                foreach (var split in currentRegistry.colorSplit)
                {
                    if (split.input == color)
                        return (split.outputA, split.outputB);
                }
            }

            return (color, color);
        }
    }

    [Serializable]
    public class ColorSplit
    {
        public LaserColor input;
        public LaserColor outputA;
        public LaserColor outputB;
    }

    [Serializable]
    public class ColorMerge
    {
        public LaserColor inputA;
        public LaserColor inputB;
        public LaserColor output;
    }
}