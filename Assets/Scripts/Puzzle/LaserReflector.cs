using Unity.Mathematics;
using UnityEngine;

namespace Puzzle
{
    public class LaserReflector : LightComponent
    {
        public Vector3 normal;
        
        public override void Propagate(LaserSegment inputSegment)
        {
            var outputDirection = Vector3.Reflect(inputSegment.Ray.direction, transform.TransformDirection(normal));
            var outSegment = new LaserSegment(new Ray(inputSegment.End, outputDirection), inputSegment.Depth + 1, inputSegment.Color);
            inputSegment.Add(outSegment);
            EmitSegment(outSegment);
        }
    }
}