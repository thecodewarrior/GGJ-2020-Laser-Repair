using System;
using UnityEngine;
using UnityEngine.U2D;

namespace Puzzle
{
    public class LaserEmitter : LightComponent
    {
        public GameObject laserSprite;
        private SpriteShapeController laserSpriteShape;

        private void Start()
        {
            laserSpriteShape = laserSprite.GetComponent<SpriteShapeController>();
        }

        public override void Propagate(LaserSegment inputSegment)
        {
            // nop
        }

        private void FixedUpdate()
        {
            var segment = new LaserSegment(new Ray(transform.position, transform.up), 0, LaserColor.NONE);
            EmitSegment(segment);
            UpdateSpriteShape(segment);
            DebugSegment(segment);
        }

        private void UpdateSpriteShape(LaserSegment segment)
        {
            laserSpriteShape.spline.Clear();

            var seg = segment;
            laserSpriteShape.spline.InsertPointAt(laserSpriteShape.spline.GetPointCount(), laserSprite.transform.InverseTransformPoint(seg.Ray.origin));
            while (seg != null)
            {
                laserSpriteShape.spline.InsertPointAt(laserSpriteShape.spline.GetPointCount(),
                    laserSprite.transform.InverseTransformPoint(seg.Ray.origin + seg.Ray.direction * (float.IsInfinity(seg.Length) ? 1000 : seg.Length)));
                
                seg = seg.Children.Count == 0 ? null : seg.Children[0];
                // segment.Color.DebugColor
            }
        }

        private void DebugSegment(LaserSegment segment)
        {
            Debug.DrawRay(segment.Ray.origin, segment.Ray.direction * (float.IsInfinity(segment.Length) ? 1000 : segment.Length), segment.Color.DebugColor);
            
            foreach (var child in segment.Children)
            {
                DebugSegment(child);
            }
        }
    }
}