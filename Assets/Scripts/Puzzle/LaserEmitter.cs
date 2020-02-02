using System;
using UnityEngine;
using UnityEngine.U2D;

namespace Puzzle
{
    public class LaserEmitter : LightComponent, ColoredObject
    {
        public LaserRenderer laserRenderer;
        public LaserColor color;
        
        public LaserColor Color
        {
            get => color;
            set => color = value;
        }

        public override void Propagate(LaserSegment inputSegment, Collider collider)
        {
            // nop
        }

        private void FixedUpdate()
        {
            var segment = new LaserSegment(new Ray(transform.position, transform.right), 0, color);
            EmitSegment(segment);
            laserRenderer.UpdateSegment(segment);
            DebugSegment(segment);
        }

        private void DebugSegment(LaserSegment segment)
        {
            Debug.DrawRay(segment.Ray.origin, segment.Ray.direction * (float.IsInfinity(segment.Length) ? 1000 : segment.Length), UnityEngine.Color.magenta);
            
            foreach (var child in segment.Children)
            {
                DebugSegment(child);
            }
        }
    }
}