using UnityEngine;

namespace Puzzle
{
    public class LaserSink : LightComponent
    {
        public override void Propagate(LaserSegment inputSegment, Collider collider)
        {
            GetComponent<FXRequests>().requests.Add("laser_wall_sizzle");
        }
    }
}