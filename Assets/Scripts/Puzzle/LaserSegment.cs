using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    public class LaserSegment
    {
        public Ray Ray;
        public int Depth;
        public LaserColor Color;
        
        public float Length = Mathf.Infinity;
        public Vector3 End => Ray.origin + Ray.direction * Length;
        
        public List<LaserSegment> Children = new List<LaserSegment>();

        public LaserSegment(Ray ray, int depth, LaserColor color)
        {
            Ray = ray;
            Depth = depth;
            Color = color;
        }

        public void Add(LaserSegment segment)
        {
            Children.Add(segment);
        }
    }
}