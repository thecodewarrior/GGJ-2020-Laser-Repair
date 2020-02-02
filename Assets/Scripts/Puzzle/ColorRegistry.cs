using System;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    public class ColorRegistry : MonoBehaviour
    {
        public LaserColor[] colors;
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
        
        public static LaserColor GetColor(string name)
        {
            if (!currentRegistry)
            {
                currentRegistry = FindObjectOfType<ColorRegistry>();
            }

            if (currentRegistry && currentRegistry.colorsByName.ContainsKey(name.ToLowerInvariant()))
                return currentRegistry.colorsByName[name.ToLowerInvariant()];
            return null;
        }
    }
}