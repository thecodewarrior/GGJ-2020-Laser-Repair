using Unity.Mathematics;
using UnityEngine;

namespace Puzzle
{
    public class LaserSplitter : LaserReflector
    {
        public override void Propagate(LaserSegment inputSegment, Collider collider)
        {
                var transformedNormal = transform.TransformDirection(normal);
            var colors = ColorRegistry.SplitColor(inputSegment.Color);
            if (math.dot(inputSegment.Ray.direction, transformedNormal) >= 0)
                colors = (colors.Item2, colors.Item1);
            {
                var outputDirection = math.reflect(inputSegment.Ray.direction, transformedNormal);
                var outSegment = new LaserSegment(
                    new Ray(inputSegment.End, outputDirection),
                    inputSegment.Depth + 1,
                    colors.Item1
                )
                {
                    EmissionNormal = math.dot(outputDirection, transformedNormal) >= 0 ? transformedNormal : -transformedNormal
                };
                inputSegment.Add(outSegment);
                EmitSegment(outSegment);
            }
            {
                var outputDirection = inputSegment.Ray.direction;
                var outSegment = new LaserSegment(
                    new Ray(inputSegment.End, outputDirection),
                    inputSegment.Depth + 1,
                    colors.Item2
                )
                {
                    EmissionNormal = math.dot(outputDirection, transformedNormal) >= 0
                        ? transformedNormal
                        : -transformedNormal
                };
                inputSegment.Add(outSegment);
                EmitSegment(outSegment);
            }
        }
    }
}