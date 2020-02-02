using Unity.Mathematics;
using UnityEngine;

namespace Puzzle
{
    public class LaserSplitter : LaserReflector
    {
        public override void Propagate(LaserSegment inputSegment)
        {
            base.Propagate(inputSegment);

            var transformedNormal = transform.TransformDirection(normal);
            var outputDirection = inputSegment.Ray.direction;
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