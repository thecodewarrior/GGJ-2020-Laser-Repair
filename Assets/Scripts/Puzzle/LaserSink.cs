using UnityEngine;

namespace Puzzle
{
    public class LaserSink : LightComponent
    {
        public override void Propagate(LaserSegment inputSegment, Collider collider)
        {
            // nop (eventually this will cause a sound)
        }
    }
}