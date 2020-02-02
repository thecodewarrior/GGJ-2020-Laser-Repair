using UnityEngine;

namespace Puzzle
{
    public class LightComponentDelegate : LightComponent
    {
        public LightComponent realComponent;
        
        public override void Propagate(LaserSegment inputSegment, Collider collider)
        {
            realComponent.Propagate(inputSegment, collider);
        }
    }
}