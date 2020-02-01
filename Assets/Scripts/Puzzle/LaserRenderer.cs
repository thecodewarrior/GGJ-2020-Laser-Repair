using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static GlobalUtils;

namespace Puzzle
{
    public class LaserRenderer : MonoBehaviour
    {
        private LaserSegment _segment;
        private MeshFilter _meshFilter;

        private void Start()
        {
            _meshFilter = GetComponent<MeshFilter>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if the laser was updated, false if the segments are identical the current segments</returns>
        public bool UpdateSegment(LaserSegment segment)
        {
            if (segment.IsIdentical(_segment))
                return false;
            _segment = segment;
            
            var vertices = new List<Vector3>(segment.Count * 4);
            var normals = new List<Vector3>(segment.Count * 4);
            var uvs = new List<Vector2>(segment.Count * 4);
            var colors = new List<Color>(segment.Count * 4);
            
            var triangles = new List<int>(segment.Count * 6);

            AddSegment(segment, Camera.main.transform.forward, vertices, normals, uvs, colors, triangles);

            var mesh = _meshFilter.mesh;
            mesh.Clear();
            mesh.vertices = vertices.ToArray();
            mesh.normals = normals.ToArray();
            mesh.uv = uvs.ToArray();
            mesh.colors = colors.ToArray();
            mesh.triangles = triangles.ToArray();

            return true;
        }

        private void AddSegment(LaserSegment segment, float3 cameraLook,
            List<Vector3> vertices, List<Vector3> normals, List<Vector2> uvs, List<Color> colors, List<int> triangles)
        {
            var transform = this.transform;
            
            float3 start = segment.Ray.origin;
            float3 direction = segment.Ray.direction;
            var distance = math.isinf(segment.Length) ? 1000 : segment.Length;
            var end = start + direction * distance;
            
            var up = math.cross(direction, cameraLook);
            var top = start + up * segment.Width / 2;
            var bottom = start - up * segment.Width / 2;

            var startTopDistance = IntersectLinePlane(top, direction, start, segment.EmissionNormal);
            var startBottomDistance = IntersectLinePlane(bottom, direction, start, segment.EmissionNormal);
            var endTopDistance = IntersectLinePlane(top, direction, end, segment.ImpactNormal);
            var endBottomDistance = IntersectLinePlane(bottom, direction, end, segment.ImpactNormal);

            var startTop = math.isinf(startTopDistance) 
                ? top 
                : top + direction * startTopDistance;
            var startBottom = math.isinf(startBottomDistance) 
                ? bottom 
                : bottom + direction * startBottomDistance;
            var endTop = math.isinf(endTopDistance) 
                ? top + direction * distance 
                : top + direction * endTopDistance;
            var endBottom = math.isinf(endBottomDistance) 
                ? bottom + direction * distance 
                : bottom + direction * endBottomDistance;

            var normal = math.cross(up, direction);
            var index = vertices.Count;
            
            vertices.Add(transform.InverseTransformPoint(startTop));
            vertices.Add(transform.InverseTransformPoint(startBottom));
            vertices.Add(transform.InverseTransformPoint(endTop));
            vertices.Add(transform.InverseTransformPoint(endBottom));
            
            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);
            
            uvs.Add(vec(0, 0));
            uvs.Add(vec(1, 0));
            uvs.Add(vec(1, 1));
            uvs.Add(vec(0, 1));
            
            colors.Add(segment.Color.beamColor);
            colors.Add(segment.Color.beamColor);
            colors.Add(segment.Color.beamColor);
            colors.Add(segment.Color.beamColor);
            
            triangles.Add(index + 1); // startBottom
            triangles.Add(index + 2); // endTop
            triangles.Add(index + 0); // startTop
            
            triangles.Add(index + 1); // startBottom
            triangles.Add(index + 3); // endBottom
            triangles.Add(index + 2); // endTop
            
            foreach (var child in segment.Children)
            {
                AddSegment(child, cameraLook, vertices, normals, uvs, colors, triangles);
            }
        }

        private float IntersectLinePlane(float3 lineOrigin, float3 lineDirection, float3 planeOrigin,
            float3 planeNormal)
        {
            var denom = math.dot(lineDirection, planeNormal);
            if (Math.Abs(denom) < Mathf.Epsilon)
            {
                return Mathf.Infinity;
            }

            return math.dot(planeOrigin - lineOrigin, planeNormal) / denom;
        }
    }
}