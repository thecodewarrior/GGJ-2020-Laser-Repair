using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using static GlobalUtils;

namespace Puzzle
{
    public class LaserRenderer : MonoBehaviour
    {
        private LaserSegment _segment;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;

        private void Start()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if the laser was updated, false if the segments are identical the current segments</returns>
        public bool UpdateSegment(LaserSegment rootSegment)
        {
            if (rootSegment.IsIdentical(_segment))
                return false;
            _segment = rootSegment;
            var mesh = _meshFilter.mesh;
            mesh.Clear();

            var segments = new Dictionary<LaserColor, List<LaserSegment>>();

            AddSegments(rootSegment, segments);

            var segmentCount = segments.Sum(it => it.Value.Count);
            
            var vertices = new List<Vector3>(segmentCount * 4);
            var normals = new List<Vector3>(segmentCount * 4);
            var uvs = new List<Vector2>(segmentCount * 4);
            
            // var triangles = new List<int>(segmentCount * 6);

            mesh.subMeshCount = segments.Count;

            var entries = segments.Select(kp =>
                (Color: kp.Key, Segments: kp.Value, Triangles: new List<int>(kp.Value.Count * 6))
            ).ToList();

            var subTriangles = new List<List<int>>();
            var cameraLook = Camera.main.transform.forward;
            for (var i = 0; i < entries.Count; i++)
            {
                var entry = entries[i];
                foreach (var segment in entry.Segments)
                {
                    DrawSegment(segment, cameraLook, vertices, normals, uvs, entry.Triangles);
                }
            }

            mesh.vertices = vertices.ToArray();
            mesh.normals = normals.ToArray();
            mesh.uv = uvs.ToArray();
            
            for (var i = 0; i < entries.Count; i++)
            {
                var entry = entries[i];
                mesh.SetTriangles(entry.Triangles.ToArray(), i);
            }

            _meshRenderer.materials = entries.Select(entry => entry.Color.material).ToArray();
            

            return true;
        }

        private void AddSegments(LaserSegment segment, Dictionary<LaserColor, List<LaserSegment>> segments)
        {
            if (segment.Color)
            {
                if (!segments.ContainsKey(segment.Color))
                    segments[segment.Color] = new List<LaserSegment>();
                segments[segment.Color].Add(segment);
            }

            foreach (var child in segment.Children)
            {
                AddSegments(child, segments);
            }
        }

        private void DrawSegment(LaserSegment segment, float3 cameraLook,
            List<Vector3> vertices, List<Vector3> normals, List<Vector2> uvs, List<int> triangles)
        {
            var transform = this.transform;
            
            float3 start = segment.Ray.origin;
            float3 direction = segment.Ray.direction;
            var distance = math.isinf(segment.Length) ? 1000 : segment.Length;
            var end = start + direction * distance;
            
            var up = math.cross(direction, cameraLook);
            var top = start + up * segment.Color.width / 2;
            var bottom = start - up * segment.Color.width / 2;

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
            uvs.Add(vec(0, 1));
            uvs.Add(vec(1, 0));
            uvs.Add(vec(1, 1));
            
            triangles.Add(index + 1); // startBottom
            triangles.Add(index + 2); // endTop
            triangles.Add(index + 0); // startTop
            
            triangles.Add(index + 1); // startBottom
            triangles.Add(index + 3); // endBottom
            triangles.Add(index + 2); // endTop
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