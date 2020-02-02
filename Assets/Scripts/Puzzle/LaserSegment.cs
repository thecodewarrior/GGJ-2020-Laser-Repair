using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Puzzle
{
    public class LaserSegment
    {
        public Ray Ray;
        public int Depth;
        public LaserColor Color;
        
        public float3 EmissionNormal; 
        public float Length = Mathf.Infinity;
        public float3 ImpactNormal; 
        public float3 End => Ray.origin + Ray.direction * Length;
        
        public List<LaserSegment> Children = new List<LaserSegment>();

        public LaserSegment(Ray ray, int depth, LaserColor color)
        {
            Ray = ray;
            EmissionNormal = ray.direction;
            ImpactNormal = -EmissionNormal;
            Depth = depth;
            Color = color;
        }

        public void Add(LaserSegment segment)
        {
            Children.Add(segment);
        }

        public bool IsIdentical(LaserSegment other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!(Ray.Equals(other.Ray) &&
                  Depth == other.Depth &&
                  Equals(Color, other.Color) &&
                  EmissionNormal.Equals(other.EmissionNormal) &&
                  Length.Equals(other.Length) &&
                  ImpactNormal.Equals(other.ImpactNormal) &&
                  Children.Count == other.Children.Count
                ))
            {
                return false;
            }

            for (int i = 0; i < Children.Count; i++)
            {
                if (!Children[i].IsIdentical(other.Children[i]))
                    return false;
            }

            return true;
        }
    }
}