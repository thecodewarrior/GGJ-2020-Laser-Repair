using Unity.Mathematics;
using UnityEngine;

namespace Puzzle
{
    public class LaserReflector : LightComponent
    {
        public Vector3 normal;
        public LaserColor colorOverride;
        
        public override void Propagate(LaserSegment inputSegment, Collider collider)
        {
            var transformedNormal = transform.TransformDirection(normal);
            var outputDirection = math.reflect(inputSegment.Ray.direction, transformedNormal);
            var outSegment = new LaserSegment(
                new Ray(inputSegment.End, outputDirection),
                inputSegment.Depth + 1,
                colorOverride ? colorOverride : inputSegment.Color
            )
            {
                EmissionNormal = math.dot(outputDirection, transformedNormal) >= 0 ? transformedNormal : -transformedNormal
            };
            inputSegment.Add(outSegment);
            EmitSegment(outSegment);
        }
    }
}