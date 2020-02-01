using UnityEngine;

namespace Puzzle
{
    public abstract class LightComponent : MonoBehaviour
    {
        /**
         * Get a set of output rays given an input ray
         */
        public abstract void Propagate(LaserSegment inputSegment);
        
        public void EmitSegment(LaserSegment segment)
        {
            RaycastHit hitInfo;
            bool hit;
            Ray hitRay = segment.Ray;
            while(true)
            {
                hit = Physics.Raycast(hitRay, out hitInfo, Mathf.Infinity, (int) GameObjectLayer.LayerComponent);
                
                if (hit && hitInfo.collider.gameObject == gameObject) // self-collision
                {
                    hitRay.origin = hitInfo.point + hitRay.direction * 0.001f;
                }
                else
                {
                    break;
                }
            }
            if (hit)
            {
                // hitInfo.direction will be incorrect if there were self-collisions, so we calculate it
                segment.Length = (hitInfo.point - segment.Ray.origin).magnitude;
                segment.ImpactNormal = hitInfo.normal;
                var lightComponent = hitInfo.collider.gameObject.GetComponent<LightComponent>();
                if (lightComponent && segment.Depth < 100)
                {
                    lightComponent.Propagate(segment);
                }
            }
            else
            {
                segment.Length = Mathf.Infinity;
            }
        }
    }
}
