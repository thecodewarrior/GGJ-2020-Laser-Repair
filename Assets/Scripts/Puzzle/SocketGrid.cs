using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Puzzle
{
    public class SocketGrid : MonoBehaviour
    {
        public string gridName;
        public float increments;
        
        public Plane GridPlane => new Plane(transform.forward, transform.position);

        public Vector3 Snap(Vector3 point)
        {
             var gridLocation = transform.InverseTransformPoint(point);
             gridLocation.x = mathu.round(gridLocation.x, increments);
             gridLocation.y = mathu.round(gridLocation.y, increments);
             return transform.TransformPoint(gridLocation);
        }

        public static SocketGrid Find(string name)
        {
            return FindObjectsOfType<SocketGrid>().FirstOrDefault(grid => grid.gridName == name);
        }
    }
}