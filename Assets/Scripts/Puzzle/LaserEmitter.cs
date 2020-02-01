using System;
using UnityEngine;
using UnityEngine.U2D;

namespace Puzzle
{
    public class LaserEmitter : LightComponent
    {
        public LaserRenderer laserRenderer;
        public LaserColor color;

        public override void Propagate(LaserSegment inputSegment)
        {
            // nop
        }

        private void FixedUpdate()
        {
            var segment = new LaserSegment(new Ray(transform.position, transform.up), 0, color);
            EmitSegment(segment);
            laserRenderer.UpdateSegment(segment);
            DebugSegment(segment);
        }

        private void DebugSegment(LaserSegment segment)
        {
            Debug.DrawRay(segment.Ray.origin, segment.Ray.direction * (float.IsInfinity(segment.Length) ? 1000 : segment.Length), Color.magenta);
            
            foreach (var child in segment.Children)
            {
                DebugSegment(child);
            }
        }
    }
}